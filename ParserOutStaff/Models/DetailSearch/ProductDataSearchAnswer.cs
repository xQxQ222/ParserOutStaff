using ParserOutStaff.ProductData;

namespace ParserOutStaff.Models.DetailSearch
{
    public class ProductDataSearchAnswer: BaseModel
    {
        public List<DetailParametersProduct> Products { get; set; }
    }
}
