using System.Collections.Generic;
using System.Net;

namespace Hubtel.Wallets.Application.Models
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}