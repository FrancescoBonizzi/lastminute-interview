using SalesTaxes_Library;
using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Presentation;
using SalesTaxes_Library.Presentation.Domain;
using System.Diagnostics;

namespace SalesTaxes_ClientConsole
{
    /// <summary>
    /// The first example of the interview document
    /// </summary>
    public class InterviewInput3 : IInterviewInput
    {
        private readonly ShoppingCartEditor _shoppingCartEditor;
        private readonly ReceiptGenerator _receiptGenerator;

        public InterviewInput3(
            ShoppingCartEditor shoppingCartEditor,
            ReceiptGenerator receiptGenerator)
        {
            _shoppingCartEditor = shoppingCartEditor;
            _receiptGenerator = receiptGenerator;
        }

        public void Run()
        {
            // Cart generation
            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 0,
                    name: "Imported bottle of perfume",
                    singleItemPrice: 27.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: true),
                quantity: 1);

            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Bottle of perfume",
                    singleItemPrice: 18.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: false),
                quantity: 1);

            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 2,
                    name: "Packet of headache pills",
                    singleItemPrice: 9.75M,
                    articleType: ArticleTypes.Medical,
                    isImported: false),
                quantity: 1);

            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 3,
                    name: "Imported box of chocolates",
                    singleItemPrice: 11.25M,
                    articleType: ArticleTypes.Food,
                    isImported: true),
                quantity: 1);

            // Receipt generation
            var receipt = _receiptGenerator.GenerateReceipt(
                shoppingCart: _shoppingCartEditor.ShoppingCart,
                receiptCustomer: new ReceiptCustomer(
                    name: "lastminute.com",
                    address: "Vicolo de’ Calvi, 2, 6830 Chiasso, Svizzera",
                    email: "testemail@test.it"));

            var htmlReceiptPath = _receiptGenerator.ExportReceiptAsHtml(
                receipt: receipt,
                destinationFolder: @"C:\Users\francescobo\LastMinuteReceipts\InterviewInput3");

            // Opens it in the default browser
            Process.Start(@"cmd.exe ", $"/c {htmlReceiptPath}");
        }
    }
}
