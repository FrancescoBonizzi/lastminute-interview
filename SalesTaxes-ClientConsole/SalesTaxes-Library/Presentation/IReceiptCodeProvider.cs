namespace SalesTaxes_Library.Presentation
{
    /// <summary>
    /// An abstraction that should provide a unique receipt code
    /// </summary>
    public interface IReceiptCodeProvider
    {
        string GetNewReceiptCode();
    }
}
