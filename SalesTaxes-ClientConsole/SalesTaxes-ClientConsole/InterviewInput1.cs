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
    public class InterviewInput1 : IInterviewInput
    {
        private readonly ShoppingCartEditor _shoppingCartEditor;
        private readonly ReceiptGenerator _receiptGenerator;

        public InterviewInput1(
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
                    name: "Book",
                    singleItemPrice: 12.49M,
                    articleType: ArticleTypes.Book,
                    isImported: false),
                quantity: 1);

            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Music CD",
                    singleItemPrice: 14.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: false),
                quantity: 1);

            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Ciocolate Bar",
                    singleItemPrice: 0.85M,
                    articleType: ArticleTypes.Food,
                    isImported: false),
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
                destinationFolder: @"C:\Users\francescobo\LastMinuteReceipts\InterviewInput1");

            // Opens it in the default browser
            Process.Start(@"cmd.exe ", $"/c {htmlReceiptPath}");
        }
    }
}
