using ParserOutStaff.ProductData;

namespace ParserOutStaff.Models.ProductSearch
{
    public class ProductSearchAnswer : BaseModel
    {
        public List<Variant> variants { get; set; } = new List<Variant>();

        public class Variant
        {
            public string phrase { get; set; } = string.Empty;
            public List<Product> products { get; set; } = new List<Product>();
        }
    }
}
