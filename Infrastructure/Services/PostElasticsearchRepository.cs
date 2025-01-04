using Application.Contracts.Infrastructure;
using Application.Dtos;
using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Infrastructure.Services
{
    public class PostElasticsearchRepository(ElasticsearchClient elasticsearchClient) : IPostElasticsearchRepository
    {
        private const string indexName = "posts";

        public async Task SavePostAsync(PostDto postDto)
        {
            await elasticsearchClient.IndexAsync(postDto, x => x.Index(indexName).Id(postDto.Id));
        }

        public async Task UpdateAsync(PostDto postDto)
        {
            await elasticsearchClient.UpdateAsync<Post, PostDto>(indexName, postDto.Id, x => x.Doc(postDto));
        }

        public async Task DeleteAsync(Guid id)
        {
            await elasticsearchClient.DeleteAsync<Post>(id, x => x.Index(indexName));
        }

        public async Task<(List<PostDto> list, long count)> SearchAsync(PostSearchDto postSearchDto, int page, int pageSize)
        {
            List<Action<QueryDescriptor<PostDto>>> queries = [];

            if (postSearchDto is null)
            {
                Action<QueryDescriptor<PostDto>> query = q => q.MatchAll(m => { });
                queries.Add(query);

                return await CalculateResultSetAsync(elasticsearchClient, page, pageSize, queries);
            }

            if (!string.IsNullOrEmpty(postSearchDto.Title))
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Match(m => m
                    .Field(f => f.Title)
                    .Query(postSearchDto.Title));

                queries.Add(query);
            }

            if (!string.IsNullOrEmpty(postSearchDto.Content))
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Match(m => m
                    .Field(f => f.Content)
                    .Query(postSearchDto.Content));

                queries.Add(query);
            }

            if (postSearchDto.CreatedDate.HasValue)
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Range(r => r
                                                                        .DateRange(dr => dr
                                                                            .Field(f => f.CreatedDate)
                                                                            .Gte(postSearchDto.CreatedDate.Value)
                                                                        )
                                                                    );
                queries.Add(query);
            }

            if (postSearchDto.LastModifiedDate.HasValue)
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Range(r => r
                                                                        .DateRange(dr => dr
                                                                            .Field(f => f.LastModifiedDate)
                                                                            .Gte(postSearchDto.LastModifiedDate.Value)
                                                                        )
                                                                    );
                queries.Add(query);
            }

            if (!string.IsNullOrEmpty(postSearchDto.AuthorFullName))
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Match(m => m
                    .Field(f => f.Author.FullName)
                    .Query(postSearchDto.AuthorFullName));

                queries.Add(query);
            }

            if (!string.IsNullOrEmpty(postSearchDto.AuthorUserName))
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Match(m => m
                    .Field(f => f.Author.Username)
                    .Query(postSearchDto.AuthorUserName));

                queries.Add(query);
            }

            if (!string.IsNullOrEmpty(postSearchDto.TagName))
            {
                Action<QueryDescriptor<PostDto>> query = q => q.Match(m => m
                    .Field(f => f.Tags.Select(t => t.TagName))
                    .Query(postSearchDto.TagName));

                queries.Add(query);
            }

            if (!queries.Any())
            {
                Action<QueryDescriptor<PostDto>> query = q => q.MatchAll(m => { });
                queries.Add(query);
            }

            return await CalculateResultSetAsync(elasticsearchClient, page, pageSize, queries);
        }

        private static async Task<(List<PostDto> list, long count)> CalculateResultSetAsync(ElasticsearchClient elasticsearchClient, int page, int pageSize, List<Action<QueryDescriptor<PostDto>>> queries)
        {
            var pageFrom = (page - 1) * pageSize;

            var response = await elasticsearchClient.SearchAsync<PostDto>(s => s
              .Index(indexName)
              .Size(pageSize)
              .From(pageFrom)
            .Query(q => q
                  .Bool(b => b
                      .Must(queries.ToArray())
                  )
              )
          );

            //foreach (var hit in response.Hits) hit.Source.Id = hit.Id;

            return (list: response.Documents.ToList(), count: response.Total);
        }
    }
}
