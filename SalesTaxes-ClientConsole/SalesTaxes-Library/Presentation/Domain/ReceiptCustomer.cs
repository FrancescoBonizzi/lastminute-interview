namespace SalesTaxes_Library.Presentation.Domain
{
    /// <summary>
    /// The customer's informations
    /// </summary>
    public class ReceiptCustomer
    {
        public ReceiptCustomer(
            string name,
            string address,
            string email)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or empty", nameof(name));
            }

            Name = name;

            // The other parameters are optional
            Address = address;
            Email = email;
        }

        public string Name { get; }
        public string Address { get; }
        public string Email { get; }
    }
}
