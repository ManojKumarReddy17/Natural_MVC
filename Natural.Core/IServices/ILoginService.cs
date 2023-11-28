
using Natural.Core.Models;

namespace Natural.Core.IServices
{
    public interface ILoginService
    {
        public  Task<LoginModel> LoginAsync(LoginModel model);

    }
}
