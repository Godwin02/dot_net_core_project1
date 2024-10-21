using dot_net_core_project1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
namespace dot_net_core_project1.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View("HomePage");
        }

       
        public IActionResult navbar()
        {
            return View("navbar");
        }
        public IActionResult FirstPage()
        {
            return View("FirstPage");
        }
        public async Task< IActionResult> SecondPage()
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IEnumerable<Product> prd = null;
            HttpResponseMessage res = await http.GetAsync("https://localhost:7024/api/Product_table");
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<IList<Product>>();
                prd = data.Result;
            }

            return View("SecondPage",prd);
        }
        public async Task<IActionResult> ThirdPage()
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<Product> prd = null;
            HttpResponseMessage res = await http.GetAsync("https://localhost:7024/api/Product_table");
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<IList<Product>>();
                prd = data.Result;
            }
            return View("ThirdPage",prd);
        }
        public async Task<IActionResult> AddProduct(Product prd)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await http.PostAsJsonAsync("https://localhost:7024/api/Product_table",prd);

            if (response.IsSuccessStatusCode) { 
                    return View("HomePage");
            }
            else
            {
                return View("FirstPage");
            }
        }

        public async Task<IActionResult> deleteProduct(int pid)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await http.DeleteAsync("https://localhost:7024/api/Product_table/" + pid);

            return RedirectToAction("SecondPage");

        }

        public async Task<IActionResult> updateData(int pid)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Product product = new Product();
            HttpResponseMessage res = await http.GetAsync("https://localhost:7024/api/Product_table/" + pid);

            string data = await res.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<Product>(data);
            return View("UpdateData", product);
        }

        public async Task<IActionResult> UpdateProduct(Product prd)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));

            HttpResponseMessage res = await http.PutAsJsonAsync("https://localhost:7024/api/Product_table/" + prd.productId,prd);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("ThirdPage");
            }
            else
            {
                return View("HomePage");
            }

        }
    }
}
