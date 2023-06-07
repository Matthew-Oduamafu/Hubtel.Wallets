using System;

#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class TblTtype
    {
        public int TIdpk { get; set; }
        public string TTypeName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}