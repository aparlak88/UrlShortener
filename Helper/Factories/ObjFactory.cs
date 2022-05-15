using Models.Concerete;

namespace Helper.Factories;

public class ObjFactory
{
    public ObjFactory()
    {

    }
    public UrlShorteningModel CreateUrlShorteningModel(string initialUrl,
        string? shorteningTag = null)
    {
        if (string.IsNullOrEmpty(initialUrl) || !UrlValidator.IsUrlValid(initialUrl))
            throw new UrlNotValidException("The URL provided is not valid.");

        if (!string.IsNullOrEmpty(shorteningTag))
        {
            if (UrlValidator.IsShorteningTagExists(shorteningTag))
                throw new ShorteningTagExistsException("The tag you requested is already occupied.");
        }

        return new UrlShorteningModel
        {
            InitialUrl = initialUrl,
            ShorteningTag = (string.IsNullOrEmpty(shorteningTag)) ? HashGenerator.GenerateHashString(initialUrl) : shorteningTag,
            Message = "Your URL successfully shortened.",
            DateInfo = DateTime.Now
        };
    }
}
