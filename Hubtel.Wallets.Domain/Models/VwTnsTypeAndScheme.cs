#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class VwTnsTypeAndScheme
    {
        public int AsIdpk { get; set; }
        public int TIdpk { get; set; }
        public int AsTypeIdfk { get; set; }
        public string TTypeName { get; set; }
        public string AsSchemeName { get; set; }
    }
}
