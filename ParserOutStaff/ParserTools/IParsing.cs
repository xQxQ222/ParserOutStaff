using ParserOutStaff.Models.DetailSearch;
using ParserOutStaff.Models.ProductSearch;
using ParserOutStaff.ProductData;

namespace ParserOutStaff.ParserTools
{
    public interface IParsing
    {
        public Task<ProductSearchAnswer> Parse(ProductSearchRequest requestedData);
        public Task<ProductDataSearchAnswer> ParseWithDetails(DetailSearchParams requestedDetailedData);
    }
}
