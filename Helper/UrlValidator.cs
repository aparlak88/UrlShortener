using DataAccess;

namespace Helper;

internal sealed class UrlValidator
{
    private UrlValidator() { }
    private static UrlValidator instance = null;
    public static UrlValidator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UrlValidator();
            }
            return instance;
        }
    }

    private const string conStr = "yoursqlserverconnectionstring";
    public static bool IsUrlValid(string UrlString)
    {
        return Uri.TryCreate(UrlString, UriKind.Absolute, out Uri uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    public static bool IsShorteningTagExists(string shorteningTag)
    {
        using (var unitOfWork = new UnitOfWork(
            new UrlShortenerContext(conStr)))
        {
            return unitOfWork.UrlShortenings.Find(x => x.ShorteningTag == shorteningTag).Any();
        }
    }
}
