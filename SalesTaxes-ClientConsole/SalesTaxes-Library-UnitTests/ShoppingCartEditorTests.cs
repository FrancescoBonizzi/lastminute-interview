using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxes_Library;
using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Storage;
using System.Linq;

namespace SalesTaxes_Library_UnitTests
{
    [TestClass]
    public class ShoppingCartEditorTests
    {
        [TestMethod]
        public void New_ShoppingCart_Should_Be_Empty_When_Created()
        {
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            ShoppingCartEditor shoppingCartEditor = new ShoppingCartEditor(
                shoppingCartRepository,
                shopConfigurationRepository);

            // With a new default constructor shopping cart editor, a new shopping cart must be created 
            Assert.IsNotNull(shoppingCartEditor.ShoppingCart);

            // A new shopping cart should create a new list of articles
            Assert.IsNotNull(shoppingCartEditor.ShoppingCart.Articles);

            // A new shopping cart should not have any articles
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Articles.Count == 0);

            // A new shopping cart should not have any totals yet
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Total == 0);

            // A new shopping cart should not have any taxes yet
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.SalesTaxes == 0);
        }

        [TestMethod]
        public void Adding_Items_To_ShoppingCart_Should_Lead_To_Increased_ArticlesCount()
        {
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            ShoppingCartEditor shoppingCartEditor = new ShoppingCartEditor(
                shoppingCartRepository,
                shopConfigurationRepository);

            shoppingCartEditor.AddArticle(new Article(0, "TestArticle #1", 12.0M, ArticleTypes.NonCategorized, false), 1);
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Articles.Count == 1);

            shoppingCartEditor.AddArticle(new Article(0, "TestArticle #2", 13.0M, ArticleTypes.NonCategorized, false), 1);
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Articles.Count == 2);
        }

        [TestMethod]
        public void Removing_Items_To_ShoppingCart_Should_Lead_To_Decreased_ArticlesCount()
        {
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            ShoppingCartEditor shoppingCartEditor = new ShoppingCartEditor(
                shoppingCartRepository,
                shopConfigurationRepository);

            shoppingCartEditor.AddArticle(new Article(0, "TestArticle #1", 12.0M, ArticleTypes.NonCategorized, false), 1);
            shoppingCartEditor.AddArticle(new Article(0, "TestArticle #2", 13.0M, ArticleTypes.NonCategorized, false), 1);
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Articles.Count == 2);

            shoppingCartEditor.RemoveArticle(shoppingCartEditor.ShoppingCart.Articles[0]);
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Articles.Count == 1);

            shoppingCartEditor.RemoveArticle(shoppingCartEditor.ShoppingCart.Articles[0]);
            Assert.IsTrue(shoppingCartEditor.ShoppingCart.Articles.Count == 0);
        }

        [TestMethod]
        public void Exercise_Input1_Should_Match_Output1()
        {
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            ShoppingCartEditor shoppingCartEditor = new ShoppingCartEditor(
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
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 12.49M);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Music CD",
                    singleItemPrice: 14.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: false),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 16.49M);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Ciocolate Bar",
                    singleItemPrice: 0.85M,
                    articleType: ArticleTypes.Food,
                    isImported: false),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 0.85M);

            Assert.AreEqual(shoppingCartEditor.ShoppingCart.SalesTaxes, 1.50M);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Total, 29.83M);
        }

        [TestMethod]
        public void Exercise_Input2_Should_Match_Output2()
        {
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            ShoppingCartEditor shoppingCartEditor = new ShoppingCartEditor(
                shoppingCartRepository,
                shopConfigurationRepository);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 0,
                    name: "Imported box of chocolates",
                    singleItemPrice: 10.00M,
                    articleType: ArticleTypes.Food,
                    isImported: true),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 10.50M);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Imported bottle of perfume",
                    singleItemPrice: 47.50M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: true),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 54.65M);

            Assert.AreEqual(shoppingCartEditor.ShoppingCart.SalesTaxes, 7.65M);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Total, 65.15M);
        }

        [TestMethod]
        public void Exercise_Input3_Should_Match_Output3()
        {
            IConfigurationRepository shopConfigurationRepository = new InMemoryConfigurationRepository();
            IShoppingCartRepository shoppingCartRepository = new InMemoryShoppingCartRepository();
            ShoppingCartEditor shoppingCartEditor = new ShoppingCartEditor(
                shoppingCartRepository,
                shopConfigurationRepository);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 0,
                    name: "Imported bottle of perfume",
                    singleItemPrice: 27.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: true),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 32.19M);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 1,
                    name: "Bottle of perfume",
                    singleItemPrice: 18.99M,
                    articleType: ArticleTypes.NonCategorized,
                    isImported: false),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 20.89M);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 2,
                    name: "Packet of headache pills",
                    singleItemPrice: 9.75M,
                    articleType: ArticleTypes.Medical,
                    isImported: false),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 9.75M);

            shoppingCartEditor.AddArticle(
                article: new Article(
                    id: 3,
                    name: "Imported box of chocolates",
                    singleItemPrice: 11.25M,
                    articleType: ArticleTypes.Food,
                    isImported: true),
                quantity: 1);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Articles.Last().TotalPrice, 11.85M);

            Assert.AreEqual(shoppingCartEditor.ShoppingCart.SalesTaxes, 6.70M);
            Assert.AreEqual(shoppingCartEditor.ShoppingCart.Total, 74.68M);
        }
    }
}