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
        public async Task<ActionResult<string>> GetPurchasesFromCns([FromBody] ProductSearchRequest requestedData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parsedItems = await _parser.Parse(requestedData);

            return Ok(JsonConvert.SerializeObject(parsedItems));
        }

        [HttpPost("CnsTehnologi/GetProductsData")]
        public async Task<ActionResult<string>> GetPurchasesDetailsFromCns([FromBody] DetailSearchParams requestedDetailedData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var detailedProductAnswer= await _parser.ParseWithDetails(requestedDetailedData);
            
            
            return Ok(JsonConvert.SerializeObject(detailedProductAnswer));
        }
    }
}
