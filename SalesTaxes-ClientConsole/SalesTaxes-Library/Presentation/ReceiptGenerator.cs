using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SalesTaxes_Library.Domain;
using SalesTaxes_Library.Presentation.Domain;
using SalesTaxes_Library.Storage;
using System;
using System.IO;
using System.Linq;

namespace SalesTaxes_Library.Presentation
{
    /// <summary>
    /// Generates and exports a printable receipt
    /// </summary>
    public class ReceiptGenerator
    {
        private readonly string _htmlTemplate;
        private readonly string _htmlTemplateCssPath;
        private readonly string _htmlTemplateJavascriptPath;
        private readonly ReceiptFormatter _receiptFormatter;
        private readonly IReceiptCodeProvider _receiptCodeProvider;
        private readonly ShopConfiguration _shopConfiguration;
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            // Because in javascript the property names are expected to be with this casing
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private const string _htmlTemplateFileName = "receipt-template.html";
        private const string _htmlTemplateCssFileName = "receipt-template-style.css";
        private const string _htmlTemplateJsFileName = "receipt-template-loading.js";

        public ReceiptGenerator(
            ReceiptFormatter receiptFormatter,
            IConfigurationRepository configurationRepository,
            IReceiptCodeProvider receiptCodeProvider)
        {
            _receiptFormatter = receiptFormatter;
            _receiptCodeProvider = receiptCodeProvider;
            _shopConfiguration = configurationRepository.Get();

            var templateFolderFullPath = Path.GetFullPath(_shopConfiguration.HtmlReceiptTemplateFoldersPath);

            if (string.IsNullOrEmpty(templateFolderFullPath))
            {
                throw new ArgumentException(
                    $"'{nameof(templateFolderFullPath)}' cannot be null or empty", 
                    nameof(templateFolderFullPath));
            }

            if(!Directory.Exists(templateFolderFullPath))
            {
                throw new ArgumentException(
                    $"Directory {templateFolderFullPath} does not exists.",
                    nameof(templateFolderFullPath));
            }

            _htmlTemplate = File.ReadAllText(Path.Combine(templateFolderFullPath, _htmlTemplateFileName));
            _htmlTemplateCssPath = Path.Combine(templateFolderFullPath, _htmlTemplateCssFileName);
            _htmlTemplateJavascriptPath = Path.Combine(templateFolderFullPath, _htmlTemplateJsFileName);
        }

        /// <summary>
        /// Given a <see cref="ShoppingCart"/> it generates a <see cref="Receipt"/> object 
        /// </summary>
        public Receipt GenerateReceipt(
            ShoppingCart shoppingCart,
            ReceiptCustomer receiptCustomer)
        {
            return new Receipt(
                receiptCode: _receiptCodeProvider.GetNewReceiptCode(),
                articles: shoppingCart.Articles.Select(article => new ReceiptArticle(
                    name: article.Name,
                    formattedQuantity: article.Quantity.ToString(),
                    formattedPrice: _receiptFormatter.FormatMoney(article.TotalPrice),
                    formattedTaxes: _receiptFormatter.FormatMoney(article.TotalTaxes))).ToList(),
                company: _shopConfiguration.ReceiptBillingCompany,
                formattedCreationDate: _receiptFormatter.FormatDate(shoppingCart.CreationDate),
                customer: receiptCustomer,
                formattedTaxesAmount: _receiptFormatter.FormatMoney(shoppingCart.SalesTaxes),
                formattedTotalAmount: _receiptFormatter.FormatMoney(shoppingCart.Total));
        }

        /// <summary>
        /// Creates an HTML receipt in the <paramref name="destinationFolder"/> 
        /// and returns its direct path
        /// </summary>
        public string ExportReceiptAsHtml(Receipt receipt, string destinationFolder)
        {
            var htmlReceipt = GenerateHtmlReceipt(receipt);

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            // Populate the template
            var htmlReceiptPath = Path.GetFullPath(Path.Combine(destinationFolder, $"{receipt.ReceiptCode}.html"));
            File.WriteAllText(path: htmlReceiptPath, contents: htmlReceipt);
            
            // Copy the javascript library
            File.Copy(
                sourceFileName: _htmlTemplateJavascriptPath,
                destFileName: Path.Combine(destinationFolder, _htmlTemplateJsFileName),
                overwrite: true);

            // Copy the syle for the template
            File.Copy(
                sourceFileName: _htmlTemplateCssPath,
                destFileName: Path.Combine(destinationFolder, _htmlTemplateCssFileName), 
                overwrite: true);

            return htmlReceiptPath;
        }

        private string GenerateHtmlReceipt(Receipt receipt)
        {
            string jsonReceipt = JsonConvert.SerializeObject(receipt, _jsonSettings);

            // If the single quote is not escaped, it could close the json string inside the javascript faile.
            jsonReceipt = jsonReceipt.Replace("'", "\'");
            var generatedHtmlReceipt = _htmlTemplate.Replace("#SHOPPING_CART_JSON_SUBSTITUTION#", jsonReceipt);

            return generatedHtmlReceipt;
        }

    }
}
