using System;

namespace SalesTaxes_Library.Presentation.Domain
{
    /// <summary>
    /// The shop billing details
    /// </summary>
    public class ReceiptBillingCompany
    {
        public ReceiptBillingCompany(
            string companyName,
            string holderName,
            string address,
            string email,
            string phone,
            string webSite)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException($"'{nameof(companyName)}' cannot be null or whitespace", nameof(companyName));
            }

            if (string.IsNullOrWhiteSpace(holderName))
            {
                throw new ArgumentException($"'{nameof(holderName)}' cannot be null or whitespace", nameof(holderName));
            }

            CompanyName = companyName;
            HolderName = holderName;

            // The other fields are optional
            Address = address;
            Email = email;
            Phone = phone;
            WebSite = webSite;
        }

        public string CompanyName { get; }
        public string HolderName { get; }
        public string Address { get; }
        public string Email { get; }
        public string Phone { get; }
        public string WebSite { get; }
    }
}
