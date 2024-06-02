using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class PressController : Controller
    {
        private readonly HttpClient client = null;
        private readonly string PressApiUrl = "";

        public PressController()
        {
            client = new HttpClient();
            MediaTypeWithQualityHeaderValue contentType = new("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PressApiUrl = "https://localhost:7201/odata/Presses";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(PressApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            dynamic lst = temp.value;
            List<Press> items = ((JArray)temp.value).Select(x => new Press
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
            }).ToList();

            return View(items);
        }
    }
}
