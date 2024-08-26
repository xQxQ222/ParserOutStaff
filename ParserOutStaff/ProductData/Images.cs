namespace ParserOutStaff.ProductData
{
    public class Images
    {
        public string Format { get; set; }=string.Empty;
        public string Base64Content { get; set; } = string.Empty;

        public Images(string format,string base64)
        {
            Format=format;
            Base64Content=base64;
        }
    }
}
