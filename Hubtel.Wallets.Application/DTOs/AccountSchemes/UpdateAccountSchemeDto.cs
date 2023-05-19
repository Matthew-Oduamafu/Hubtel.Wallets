using System;

namespace Hubtel.Wallets.Application.DTOs.AccountSchemes
{
    public class UpdateAccountSchemeDto : AccountSchemeGetDto
    {
        public DateTime EditedDate { get; set; }
    }
}