namespace Helper;

public abstract class CustomException : Exception
{
    public CustomException()
    {

    }

    public CustomException(string message) : base(message)
    {

    }
}

internal class UrlNotValidException : CustomException
{
    public UrlNotValidException()
    {

    }

    public UrlNotValidException(string message) : base (message)
    {

    }
}

internal class ShorteningTagExistsException : CustomException
{
    public ShorteningTagExistsException()
    {

    }

    public ShorteningTagExistsException(string message) : base(message)
    {

    }
}
