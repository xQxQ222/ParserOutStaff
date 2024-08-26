using AngleSharp.Browser;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParserOutStaff.Models.DetailSearch;
using ParserOutStaff.Models.ProductSearch;
using ParserOutStaff.ParserTools;
using static ParserOutStaff.Models.ProductSearch.ProductSearchAnswer;

namespace ParserOutStaff.Controllers
{
    [ApiController]
    [Route("/api")]
    public class CnsController : ControllerBase
    {
        private readonly IParsing _parser;

        public CnsController(IParsing parser)
        {
            _parser = parser;
        }

        [HttpPost("CnsTehnologi/GetProducts")]
        public async Task<ActionResult<string>> GetPurchasesFromCns([FromBody] ProductSearchRequest data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phrase = data.SearchPhraseList[0].ToLower();

            var productsList = await _parser.Parse(phrase);

            Variant variant = new Variant
            {
                phrase = phrase,
                products = productsList
            };
            ProductSearchAnswer products = new ProductSearchAnswer
            {
                App = data.App,
                variants = new List<Variant> { variant }
            };

            return Ok(JsonConvert.SerializeObject(products));
        }

        [HttpPost("CnsTehnologi/GetProductsData")]
        public async Task<ActionResult<string>> GetPurchasesDetailsFromCns([FromBody] DetailSearchParams data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var link = data.ProductLinks[0];
            var productWithDetails= await _parser.ParseWithDetails(link);
            
            ProductDataSearchAnswer productDataSearchAnswer=new ProductDataSearchAnswer();
            productDataSearchAnswer.App = data.App;
            productDataSearchAnswer.Products=new List<ProductData.DetailParametersProduct> { productWithDetails };
            return Ok(JsonConvert.SerializeObject(productDataSearchAnswer));
        }
    }
}
