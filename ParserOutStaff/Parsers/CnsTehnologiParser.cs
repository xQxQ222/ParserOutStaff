using AngleSharp.Dom;
using AngleSharp;
using ParserOutStaff.ParserTools;
using ParserOutStaff.ProductData;
using AngleSharp.Text;
using ParserOutStaff.Models.ProductSearch;
using static ParserOutStaff.Models.ProductSearch.ProductSearchAnswer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ParserOutStaff.Models;
using ParserOutStaff.Models.DetailSearch;

namespace ParserOutStaff.Parsers
{
    public class CnsTehnologiParser : IParsing
    {
        private readonly IHtmlRequester _requester;

        public CnsTehnologiParser(IHtmlRequester requester)
        {
            _requester = requester;
        }

        public async Task<IDocument> GetDocument(string pageUrl)
        {
            var pageContent = await _requester.GetPage(pageUrl);
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(pageContent));
            return document;
        }

        private async Task<List<string>> FindProductsUrl()
        {
            List<string> pages = new List<string>()
            {
                "https://cnc-tehnologi.ru/stanki-dlya-obrabotki-drevesiny",
                "https://cnc-tehnologi.ru/stanki-dlya-domashnej-masterskoj",
                "https://cnc-tehnologi.ru/stanki-dlya-obrabotki-metalla",
                "https://cnc-tehnologi.ru/stanki-dlya-raskroya",
                "https://cnc-tehnologi.ru/kruglofrezernye-stanki",
                "https://cnc-tehnologi.ru/lazernye-co2-stanki",
                "https://cnc-tehnologi.ru/lazernye-stanki-po-kamnyu",
                "https://cnc-tehnologi.ru/lazernyj-gravirovshchik-stanki"
            };
            var productsUrl = new List<string>();
            foreach (var page in pages)
            {
                var document = await GetDocument(page);
                var pageContent = document.QuerySelectorAll("a.button_detail.gray.btn");
                foreach (var prod in pageContent)
                {
                    var productUrl = "https://cnc-tehnologi.ru" + prod.GetAttribute("href");
                    productsUrl.Add(productUrl);
                }
            }
            return productsUrl;
        }

        private async Task<Product> GetProductsAsync(string productUrl)
        {
            var product = new Product();
            var document = await GetDocument(productUrl);
            product.Price = decimal.Parse(document.QuerySelector("span#block_price").GetAttribute("data-price"));
            product.Link = productUrl;
            product.CatalogPath = string.Join("->", document.QuerySelector("ul.breadcrumb").TextContent.Trim().Split(new char[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            product.Code = document.GetElementById("product_id")?.GetAttribute("value");
            product.Name = document.QuerySelector("h1").InnerHtml;
            var chardf = document.QuerySelectorAll("tbody").First().QuerySelectorAll("tr");

            foreach (var e in chardf)
            {
                var g = e.TextContent.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (g.Length == 2)
                    product.Properties.Add(new Property(g[0], g[1]));
            }

            return product;
        }

        private async Task<DetailParametersProduct> AddDetails(Product product,string link)
        {
            var document=await GetDocument(link);
            var productDetail = new DetailParametersProduct(product);
            var p = document.QuerySelectorAll("div#list_product_image_middle");
            foreach (var item in p)
            {
                var imageUrl = item.QuerySelector("a").GetAttribute("href");
                productDetail.Images.Add(new Images(imageUrl.Split('.').Last(),await GetBase64Image(imageUrl)));
            }
            return productDetail;
        }

        private static async Task<string> GetBase64Image(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                byte[] imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                return Convert.ToBase64String(imageBytes);
            }
        }

        public async Task<ProductSearchAnswer> Parse(ProductSearchRequest requestedData)
        {
            var products = new List<Product>();
            var productPages = await FindProductsUrl();
            foreach (var page in productPages)
            {
               var product = await GetProductsAsync(page);
                products.Add(product);
            }
            Variant variant = new Variant
            {
                phrase = requestedData.SearchPhraseList[0],
                products = products.Where(x => x.Name.ToLower().Contains(requestedData.SearchPhraseList[0].ToLower())).ToList()
            };
            ProductSearchAnswer answer = new ProductSearchAnswer
            {
                App = requestedData.App,
                variants = new List<Variant> { variant }
            };
            return answer;
        }

        public async Task<ProductDataSearchAnswer> ParseWithDetails(DetailSearchParams requestedDetailedData)
        {
            var productWithoutDetails=await GetProductsAsync(requestedDetailedData.ProductLinks[0]);
            var productWithDetails = await AddDetails(productWithoutDetails, requestedDetailedData.ProductLinks[0]);
            ProductDataSearchAnswer productDataSearchAnswer = new ProductDataSearchAnswer()
            {
                App = requestedDetailedData.App,
                Products = new List<ProductData.DetailParametersProduct> { productWithDetails }
            };
            return productDataSearchAnswer;
        }
    }
}
