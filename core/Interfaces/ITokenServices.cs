using core.Entities.Identity;

namespace core.Interfaces
{
    public interface ITokenServices
    {
         string CreateToken(AppUser user);
         
    }
}