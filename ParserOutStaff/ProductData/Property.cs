namespace ParserOutStaff.ProductData
{
    public class Property
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public Property(string name,string value)
        {
            Name = name;
            Value = value;
        }
    }
}
