namespace SalesTaxes_Library.Domain
{
    /// <summary>
    /// An enumeration used to categorize each cart item without ambiguity 
    /// and with strong typing checks
    /// </summary>
    public enum ArticleTypes
    {
        NonCategorized = 0,

        Book = 1,
        Food = 2,
        Medical = 3
    }
}
