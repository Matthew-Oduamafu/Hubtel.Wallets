using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace Hubtel.Wallets.Application.Models.Identity
{
    public class RegistrationBadResponse : IRegistrationResponse
    {
        [DefaultValue("")]
        public string Message { get; set; }

        [DefaultValue(HttpStatusCode.BadRequest)]
        public HttpStatusCode StatusCode { get; set; }

        [DefaultValue(null)]
        public List<string> Errors { get; set; }
    }
}