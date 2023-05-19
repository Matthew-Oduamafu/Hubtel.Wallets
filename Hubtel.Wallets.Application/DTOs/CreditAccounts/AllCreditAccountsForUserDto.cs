using System;

namespace Hubtel.Wallets.Application.DTOs.CreditAccounts
{
    public class AllCreditAccountsForUserDto
    {
        public int UcaIDpk { get; set; }
        public int TIDpk { get; set; }
        public int AsIDpk { get; set; }
        public string? TTypeName { get; set; }
        public string? AsSchemeName { get; set; }
        public string? UcaAccountNumber { get; set; }
        public DateTime UcaCreationDate { get; set; }
    }
}