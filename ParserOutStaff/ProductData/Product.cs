namespace ParserOutStaff.ProductData
{
    public class Product
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; } = "RUB";
        public decimal QuantityCurrent { get; set; } = 0;
        public decimal QuantityInStock { get; set; } = 0;
        public string Link { get; set; } = string.Empty;
        public string CatalogPath { get; set; } = string.Empty;
        public List<Property> Properties { get; set; } = new List<Property>();
    }
}
