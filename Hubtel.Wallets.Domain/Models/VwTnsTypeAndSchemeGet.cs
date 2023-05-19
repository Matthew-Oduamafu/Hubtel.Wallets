#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class VwTnsTypeAndSchemeGet
    {
        public int AsIdpk { get; set; }
        public int TIdpk { get; set; }
        public string TTypeName { get; set; }
        public string AsSchemeName { get; set; }
    }
}
