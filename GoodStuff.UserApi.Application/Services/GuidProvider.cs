namespace GoodStuff.UserApi.Application.Services;

public class GuidProvider : IGuidProvider
{
    public Guid Get()
    {
        var guid = Guid.NewGuid();
        return guid;
    }
    
}