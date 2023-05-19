using System;

namespace Hubtel.Wallets.Application.DTOs.CreditAccounts
{
    public class AllCreditAccountsDto
    {
        public int UcaIdpk { get; set; }
        public string UcaUserIdfk { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int UcaTypeIdfk { get; set; }
        public int TIdpk { get; set; }
        public int AsIdpk { get; set; }
        public int UcaSchemeIdfk { get; set; }
        public string TTypeName { get; set; }
        public string AsSchemeName { get; set; }
        public string UcaAccountNumber { get; set; }
        public DateTime UcaCreationDate { get; set; }
        public DateTime? UcaEditedDate { get; set; }
    }
}