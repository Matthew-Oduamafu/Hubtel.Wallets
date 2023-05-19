using System;

#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class TblAsAccountScheme
    {
        public int AsIdpk { get; set; }
        public int AsTypeIdfk { get; set; }
        public string AsSchemeName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
