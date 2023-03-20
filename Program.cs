
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace JSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "D:\\C#\\JSON";
            Func <Products,bool> filtr = p => p.Price >= 120;
            for (int i = 1; i < 10; i++)
            {
                string filepath = Path.Combine(path, $"{i}.json");
                var products = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(filepath));
                List<Products> filtrproduct = products.Where(filtr).ToList();
               foreach (Products product in filtrproduct) 
                { Console.WriteLine(product.Name); }

            }





        }


    }
}
