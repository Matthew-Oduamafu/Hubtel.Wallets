using System;

namespace Hubtel.Wallets.Application.DTOs.CreditAccounts
{
    public class UpdateCreditAccountDto : CreateCreditAccountDto
    {
        public int UcaIdpk { get; set; }
        public DateTime? UcaEditedDate { get; set; }
    }
}