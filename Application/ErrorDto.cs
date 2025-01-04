namespace Application
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; } = [];
        public bool IsVisible { get; private set; }
        public ErrorDto(string error, bool isVisible)
        {
            Errors.Add(error);
            IsVisible = isVisible;
        }

        public ErrorDto(List<string> errors, bool isVisible)
        {
            Errors = errors;
            IsVisible = isVisible;
        }
    }
}
