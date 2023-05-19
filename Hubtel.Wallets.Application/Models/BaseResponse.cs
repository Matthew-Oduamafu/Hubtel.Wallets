using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace Hubtel.Wallets.Application.Models
{
    public class BaseResponse
    {
        [DefaultValue(0)]
        public int Id { get; set; }

        [DefaultValue(false)]
        public bool Success { get; set; }

        [DefaultValue("")]
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        [DefaultValue(null)]
        public List<string> Errors { get; set; }
    }
}