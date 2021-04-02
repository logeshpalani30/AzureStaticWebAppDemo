using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class IndexBase : ComponentBase
    {
        public IndexBase()
        {
        } 
        public List<Product> _products = new List<Product>();

        private HttpClient http = new HttpClient();

        protected override async Task OnInitializedAsync()
        {
            _products = await http.GetFromJsonAsync<List<Product>>("api/GetProducts");
            Console.WriteLine(_products);
        }

    }
}