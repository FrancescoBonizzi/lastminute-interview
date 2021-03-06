﻿using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Presentation.Domain;
using System.Collections.Generic;

namespace SalesTaxes_Library.Storage
{
    /// <summary>
    /// An in memory implementation of a shop configuration repository,
    /// just to make an example
    /// </summary>
    public class InMemoryConfigurationRepository : IConfigurationRepository
    {
        private readonly ShopConfiguration _shopConfiguration;

        public InMemoryConfigurationRepository()
        {
            _shopConfiguration = new ShopConfiguration(
                importDutyTaxPercentage: 5,
                basicSalesTax: new Dictionary<ArticleTypes, decimal>()
                {
                    [ArticleTypes.NonCategorized] = 10,
                    [ArticleTypes.Book] = 0,
                    [ArticleTypes.Food] = 0,
                    [ArticleTypes.Medical] = 0
                },
                currencySymbol: "€",
                dateFormat: "dd/MM/yyyy HH:mm",
                htmlReceiptTemplateFoldersPath: @"Presentation\ReceiptTemplates",
                new ReceiptBillingCompany(
                    companyName: "4days-Soft",
                    holderName: "Francesco Bonizzi",
                    address: "Parma, Italy",
                    email: "fbonizzi.90@gmail.com",
                    phone: null,
                    webSite: null));
        }

        public ShopConfiguration Get()
            => _shopConfiguration;
    }
}
