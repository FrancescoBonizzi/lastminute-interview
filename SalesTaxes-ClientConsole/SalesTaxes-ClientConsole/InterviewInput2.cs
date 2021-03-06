﻿using SalesTaxes_Library;
using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Presentation;
using SalesTaxes_Library.Presentation.Domain;
using System.Diagnostics;

namespace SalesTaxes_ClientConsole
{
    /// <summary>
    /// The first example of the interview document
    /// </summary>
    public class InterviewInput2 : IInterviewInput
    {
        private readonly ShoppingCartEditor _shoppingCartEditor;
        private readonly ReceiptGenerator _receiptGenerator;

        public InterviewInput2(
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
                    name: "Imported box of chocolates",
                    singleItemPrice: 10.00M,
                    articleType: ArticleTypes.Food,
                    isImported: true),
                quantity: 1);

            _shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Imported bottle of perfume",
                    singleItemPrice: 47.50M,
                    articleType: ArticleTypes.NonCategorized,
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
                destinationFolder: @"C:\Users\francescobo\LastMinuteReceipts\InterviewInput2");

            // Opens it in the default browser
            Process.Start(@"cmd.exe ", $"/c {htmlReceiptPath}");
        }
    }
}
