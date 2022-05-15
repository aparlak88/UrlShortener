namespace Helper;

internal class HashGenerator
{
    private HashGenerator() { }
    private static HashGenerator instance = null;
    public static HashGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HashGenerator();
            }
            return instance;
        }
    }
    
    public static string GenerateHashString(string initialUrl)
    {
        var allowedSymbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        var hash = new char[6];

        for (int i = 0; i < initialUrl.Length; i++)
        {
            hash[i % 6] = (char)(hash[i % 6] ^ initialUrl[i]);
        }

        for (int i = 0; i < 6; i++)
        {
            hash[i] = allowedSymbols[hash[i] % allowedSymbols.Length];
        }

        return new string(hash);
    }

}
