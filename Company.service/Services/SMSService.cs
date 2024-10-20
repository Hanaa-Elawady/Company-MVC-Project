using Company.service.Helper;
using Company.service.Interfaces;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.service.Services
{
    public class SMSService : ISMSService
    {
        private TwillioSettings _options;

        public SMSService(IOptions<TwillioSettings> options)
        {
            _options = options.Value;
        }

        public MessageResource SendSms(SMS sms)
        {
            TwilioClient.Init(_options.AccountSID , _options.AuthToken);
            var result = MessageResource.Create(
                body:sms.Body,
                to : sms.PhoneNumber,
                from :new Twilio.Types.PhoneNumber(_options.Number)
                );

            return result;  
        }
    }
}
