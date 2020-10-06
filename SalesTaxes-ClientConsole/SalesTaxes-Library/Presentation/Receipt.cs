using SalesTaxes_Library.Presentation.Domain;
using System;
using System.Collections.Generic;

namespace SalesTaxes_Library.Presentation
{
    /// <summary>
    /// An exportable object that represent a receipt that could be sent the customer
    /// and graphically represented 
    /// </summary>
    public class Receipt
    {
        public Receipt(
            string receiptCode,
            string formattedCreationDate,
            ReceiptCustomer customer,
            ReceiptBillingCompany company,
            string formattedTaxesAmount,
            string formattedTotalAmount,
            List<ReceiptArticle> articles)
        {
            if (string.IsNullOrWhiteSpace(receiptCode))
            {
                throw new ArgumentException($"'{nameof(receiptCode)}' cannot be null or whitespace", nameof(receiptCode));
            }

            if (string.IsNullOrWhiteSpace(formattedCreationDate))
            {
                throw new ArgumentException($"'{nameof(formattedCreationDate)}' cannot be null or whitespace", nameof(formattedCreationDate));
            }

            if (string.IsNullOrWhiteSpace(formattedTaxesAmount))
            {
                throw new ArgumentException($"'{nameof(formattedTaxesAmount)}' cannot be null or whitespace", nameof(formattedTaxesAmount));
            }

            if (string.IsNullOrWhiteSpace(formattedTotalAmount))
            {
                throw new ArgumentException($"'{nameof(formattedTotalAmount)}' cannot be null or whitespace", nameof(formattedTotalAmount));
            }

            ReceiptCode = receiptCode;
            FormattedCreationDate = formattedCreationDate;
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Company = company ?? throw new ArgumentNullException(nameof(company));
            FormattedTaxesAmount = formattedTaxesAmount;
            FormattedTotalAmount = formattedTotalAmount;
            Articles = articles ?? throw new ArgumentNullException(nameof(articles));

            if (articles.Count == 0)
            {
                throw new ArgumentException("A receipt cannot be created without articles", nameof(articles));
            }
        }

        // TODO Finire di commentare
        public string ReceiptCode { get; }
        public string FormattedCreationDate { get; }

        public ReceiptCustomer Customer { get; }
        public ReceiptBillingCompany Company { get; }

        public string FormattedTaxesAmount { get; }
        public string FormattedTotalAmount { get; }

        public List<ReceiptArticle> Articles { get; }

        public string FooterNotes { get; }
    }
}
