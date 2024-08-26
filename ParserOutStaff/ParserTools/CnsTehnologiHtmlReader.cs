namespace ParserOutStaff.ParserTools
{
    public class CnsTehnologiHtmlReader:IHtmlRequester
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CnsTehnologiHtmlReader(IHttpClientFactory clientFactory)
        {
            this._httpClientFactory = clientFactory;
        }

        public async Task<string> GetPage(string url)
        {
            var client = _httpClientFactory.CreateClient("CnsTehnologiClient");
            var res = await client.GetStringAsync(url);
            return res;
        }
    }
}
