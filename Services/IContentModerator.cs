namespace cognitive_services.Services
{
    public interface IContentModerator
    {
        string Text(string text);

        string Image(string url);
    }
}
