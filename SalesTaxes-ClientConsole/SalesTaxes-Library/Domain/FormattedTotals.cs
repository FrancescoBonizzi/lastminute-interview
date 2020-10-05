namespace SalesTaxes_Library.Domain
{
    /// <summary>
    /// Represents the receipt totals formatted with the shop currency
    /// </summary>
    public class FormattedTotals
    {
        public FormattedTotals(
            string formattedTaxesAmount, 
            string formattedTotalAmount)
        {
            FormattedTaxesAmount = formattedTaxesAmount;
            FormattedTotalAmount = formattedTotalAmount;
        }

        public string FormattedTaxesAmount { get; set; }
        public string FormattedTotalAmount { get; set; }
    }
}
