using SalesTaxes_Library;
using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Presentation;
using SalesTaxes_Library.Presentation.Domain;
using SalesTaxes_Library.Storage;
using System.Diagnostics;

namespace SalesTaxes_ClientConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Cart generation
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            IReceiptCodeProvider receiptCodeProvider = new RandomStringWithPrefixReceiptCodeProvider();
            var shoppingCartEditor = new ShoppingCartEditor(
                shoppingCartRepository,
                shopConfigurationRepository);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 0,
                    name: "Book",
                    singleItemPrice: 12.49M,
                    articleType: ArticleTypes.Book,
                    isImported: false),
                quantity: 1);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Music CD",
                    singleItemPrice: 14.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: false),
                quantity: 1);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Ciocolate Bar",
                    singleItemPrice: 0.85M,
                    articleType: ArticleTypes.Food,
                    isImported: false),
                quantity: 1);

            // Receipt generation
            // TODO Da configurazione, va bene anche la solita
            var receiptFormatter = new ReceiptFormatter(shopConfigurationRepository);
            var receiptTemplateLoader = new ReceiptGenerator(
                receiptFormatter,
                shopConfigurationRepository,
                receiptCodeProvider);

            var receipt = receiptTemplateLoader.GenerateReceipt(
                shoppingCart: shoppingCartEditor.ShoppingCart,
                receiptCustomer: new ReceiptCustomer(
                    name: "lastminute.com",
                    address: "Vicolo de’ Calvi, 2, 6830 Chiasso, Svizzera",
                    email: "testemail@test.it"));

            var htmlReceiptPath = receiptTemplateLoader.ExportReceiptAsHtml(
                receipt: receipt,
                destinationFolder: @"C:\Users\francescobo\LastMinuteReceipts");
            
            // Opens it in the default browser
            Process.Start(@"cmd.exe ", $"/c {htmlReceiptPath}");
        }
    }
}
