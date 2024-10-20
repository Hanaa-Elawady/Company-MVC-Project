using Company.Services.Helper;

namespace Company.service.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(Email input);
    }
}
