namespace ParserOutStaff.ParserTools
{
    public class CnsTehRequester:IHtmlRequester
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CnsTehRequester(IHttpClientFactory clientFactory)
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
