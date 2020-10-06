using System;
using System.Linq;

namespace SalesTaxes_Library.Presentation
{
    /// <summary>
    /// A very unsecure unique receipt code generator, used here just to give an example
    /// </summary>
    public class RandomStringWithPrefixReceiptCodeProvider : IReceiptCodeProvider
    {
        private static readonly Random _random = new Random();
        private const int _codeLenght = 4;

        public string GetNewReceiptCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return "R-" + new string(Enumerable.Repeat(chars, _codeLenght)
                .Select(s => s[_random.Next(s.Length)]).ToArray());

        }
    }
}
