namespace ParserOutStaff.ProductData
{
    public class DetailParametersProduct :Product
    {
        public List<Images> Images=new List<Images>();
        List<Attachments> Attachments=new List<Attachments>();

        public DetailParametersProduct(Product product)
        {
            this.Code = product.Code;
            this.Name = product.Name;
            this.Price = product.Price;
            this.PriceCurrency = product.PriceCurrency;
            this.QuantityCurrent = product.QuantityCurrent;
            this.QuantityInStock = product.QuantityInStock;
            this.CatalogPath = product.CatalogPath;
            this.Link=product.Link;
            this.Properties = product.Properties;
        }
    }
}
