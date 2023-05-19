using Newtonsoft.Json;

namespace Hubtel.Wallets.Application.Models
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}