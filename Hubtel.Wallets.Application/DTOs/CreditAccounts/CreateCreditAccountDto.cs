namespace Hubtel.Wallets.Application.DTOs.CreditAccounts
{
    public class CreateCreditAccountDto
    {
        public string UcaUserIdfk { get; set; }
        public int UcaTypeIdfk { get; set; }
        public int UcaSchemeIdfk { get; set; }

        public string UcaAccountNumber
        {
            get;

            set;
        }
    }
}