using System;

#nullable disable

namespace Hubtel.Wallets.Domain.Models
{
    public partial class TblUcaUserCreditAccount
    {
        public int UcaIdpk { get; set; }
        public string UcaUserIdfk { get; set; }
        public int UcaTypeIdfk { get; set; }
        public int UcaSchemeIdfk { get; set; }
        public string UcaAccountNumber { get; set; }
        public DateTime UcaCreationDate { get; set; }
        public DateTime? UcaEditedDate { get; set; }
    }
}
