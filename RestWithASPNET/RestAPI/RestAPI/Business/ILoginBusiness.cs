using RestAPI.Data.VO;

namespace RestAPI.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredetials(UserVO user);
        TokenVO ValidateCredetials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
