interface IThirdPartyAuth
{
    void Authenticate();
}

class GithubAuth : IThirdPartyAuth
{
    public void Authenticate()
    {
        // Authenticate via Github
    }
}

class GoogleAuth : IThirdPartyAuth
{
    public void Authenticate()
    {
        // Authenticate via Google
    }
}
