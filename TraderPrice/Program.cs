using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication.ExtendedProtection;

namespace TraderPrice
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 2;
            string str = "Части животных";
            string str2 = "Части мутантов";
            string filePath = "C:\\Users\\Зверь23\\source\\repos\\TraderPrice\\TraderPrice\\TraderPlusPriceConfig.json";
            string newFilePath = "C:\\Users\\Зверь23\\source\\repos\\TraderPrice\\TraderPrice\\NewTraderPlusPriceConfig.json";
            string json = File.ReadAllText(filePath);
            Rootobject price = JsonConvert.DeserializeObject<Rootobject>(json);

            foreach (var trader in price.TraderCategories)
            {
                if (trader.CategoryName.Contains(str) || trader.CategoryName.Contains(str2))
                {
                    for (int i = 0; i < trader.Products.Count; i++) 
                    {

                        string[] parts = trader.Products[i].Split(',');
                        string lastNumberString = parts[^1];
                        double Sell = Convert.ToDouble(lastNumberString) * x;

                        string newproduct = parts[0] + ",1,-1,1000,-1," + $"{Sell}";
                        //Console.WriteLine(trader.Products[i]);
                        trader.Products[i] = newproduct;
                        //Console.WriteLine(trader.Products[i]);
                    }
                }
            }
            string updatedJson = JsonConvert.SerializeObject(price, Formatting.Indented);
            File.WriteAllText(newFilePath, updatedJson);
        }
    }
    public class Rootobject
    {
        public string Version { get; set; }
        public int EnableAutoCalculation { get; set; }
        public int EnableAutoDestockAtRestart { get; set; }
        public int EnableDefaultTraderStock { get; set; }
        public Tradercategory[] TraderCategories { get; set; }
    }

    public class Tradercategory
    {
        public string CategoryName { get; set; }
        public List<string> Products { get; set; }
    }
}


        
