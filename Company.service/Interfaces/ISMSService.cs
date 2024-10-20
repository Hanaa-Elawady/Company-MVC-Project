using Company.service.Helper;
using Twilio.Rest.Api.V2010.Account;

namespace Company.service.Interfaces
{
    public interface ISMSService
    {
        MessageResource SendSms(SMS sms);
    }
}
