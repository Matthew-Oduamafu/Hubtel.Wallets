#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class TblAsAccountSchemeGet
    {
        public int AsIdpk { get; set; }
        public int AsTypeIdfk { get; set; }
        public string AsSchemeName { get; set; }
    }
}