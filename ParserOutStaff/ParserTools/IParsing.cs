using ParserOutStaff.ProductData;

namespace ParserOutStaff.ParserTools
{
    public interface IParsing
    {
        public Task<List<Product>> Parse(string phrase);
        public Task<DetailParametersProduct> ParseWithDetails(string link);
    }
}
