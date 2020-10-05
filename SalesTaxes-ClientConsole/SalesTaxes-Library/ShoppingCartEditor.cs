using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Storage;
using System;

namespace SalesTaxes_Library
{
    /// <summary>
    /// A shopping cart manager which has the goal to calculate a receipt
    /// </summary>
    public class ShoppingCartEditor
    {
        public ShoppingCart ShoppingCart { get; }

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ShopConfiguration _shopConfiguration;

        public ShoppingCartEditor(
            IShoppingCartRepository shoppingCartRepository,
            IConfigurationRepository configurationRepository)
        {
            _shoppingCartRepository = shoppingCartRepository
                ?? throw new ArgumentNullException(nameof(shoppingCartRepository));

            ShoppingCart = _shoppingCartRepository.New();
            _shopConfiguration = configurationRepository.Get();
        }

        public void Save()
        {
            _shoppingCartRepository.Save(ShoppingCart);
        }


        // TODO: Move it on another class like template applicator

        /// <summary>
        /// Calculates the receipt totals
        /// </summary>
        /// <returns></returns>
        //public FormattedTotals GetTotals()
        //{
        //    return new FormattedTotals(
        //        formattedTaxesAmount: $"{_shopConfiguration.CurrencySymbol} {ShoppingCart.SalesTaxes}",
        //        formattedTotalAmount: $"{_shopConfiguration.CurrencySymbol} {ShoppingCart.Total}");
        //}

        /// <summary>
        /// Adds an article to the current shopping cart
        /// </summary>
        public void AddArticle(Article article, int quantity)
        {
            var newShoppingCartArticle = new ShoppingCartArticle(
                articleId: article.Id,
                name: article.Name,
                quantity: quantity,
                singleItemPrice: article.SingleItemPrice,
                singleItemTaxes: CalculateTaxesForArticle(article),
                articleType: article.ArticleType,
                isImported: article.IsImported);

            ShoppingCart.Articles.Add(newShoppingCartArticle);

            ShoppingCart.Total += newShoppingCartArticle.TotalPrice;
            ShoppingCart.SalesTaxes += newShoppingCartArticle.TotalTaxes;

            // TODO Merge if finds the same article (distinct by ArticleId)
        }

        private decimal CalculateTaxesForArticle(Article article)
        {
            // Basic sales tax lookup based on the article type
            var thisArticleBasicSalesTaxPercentage = _shopConfiguration.BasicSalesTax[article.ArticleType];

            // Imported or not improted tax percentage
            var thisArticleDutyTaxPercentage = article.IsImported ? _shopConfiguration.ImportDutyTaxPercentage : 0M;

            // The resulting taxes percentage
            var totalTaxesPercentage = thisArticleBasicSalesTaxPercentage + thisArticleDutyTaxPercentage;

            // I don't trust "== 0" with decimal data... 
            if (totalTaxesPercentage <= 0)
                return 0M;

            decimal thisArticleTaxPercentage = ((article.SingleItemPrice) / 100M) * totalTaxesPercentage;

            // Round up to the nearest 0.05
            return thisArticleTaxPercentage.RoundUpToTheNearest005();
        }

        /// <summary>
        /// Removes an article from the shopping cart
        /// </summary>
        /// <param name="shoppingCartArticleId"></param>
        public void RemoveArticle(ShoppingCartArticle shoppingCartArticle)
        {
            ShoppingCart.Total -= shoppingCartArticle.TotalPrice;
            ShoppingCart.SalesTaxes -= shoppingCartArticle.TotalTaxes;
            ShoppingCart.Articles.Remove(shoppingCartArticle);
        }
    }
}
