using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Authentication.ExtendedProtection;

namespace TraderPrice
{
    class Program
    {
        static void Main(string[] args)
        {
            double x_farm = 1.15;
            double x_tayniki = 0.8;
            double x_obvesi = 1;
            string str = "Части животных";
            string str2 = "Части мутантов";
            string str3 = "Артефакты";
            string str4 = "Артефрукты";
            string str5 = "Обвесы";
            string str6 = "Хар с тайников";
            string str7 = "Хабар с тайников";
            string filePath = "C:\\Users\\Зверь23\\source\\repos\\TraderPrice\\TraderPrice\\TraderPlusPriceConfig.json";
            string newFilePath = "C:\\Users\\Зверь23\\source\\repos\\TraderPrice\\TraderPrice\\NewTraderPlusPriceConfig.json";
            string json = File.ReadAllText(filePath);
            Rootobject price = JsonConvert.DeserializeObject<Rootobject>(json);

            foreach (var trader in price.TraderCategories)
            {
                if (trader.CategoryName.Contains(str3) 
                    || trader.CategoryName.Contains(str4))
                {
                    rebalance_sell(x_farm, trader);
                }
                if (trader.CategoryName.Contains(str5))
                {
                    rebalance_buy(x_obvesi, trader);
                }
                if (trader.CategoryName.Contains(str6) || trader.CategoryName.Contains(str7))
                {
                    rebalance_sell(x_tayniki, trader);
                }
            }
            string updatedJson = JsonConvert.SerializeObject(price, Formatting.Indented);
            File.WriteAllText(newFilePath, updatedJson);
        }
        public static void rebalance_sell(double x, Tradercategory trader) 
        {
            for (int i = 0; i < trader.Products.Count; i++)
            {

                string[] parts = trader.Products[i].Split(',');
                string lastNumberString = parts[^1];
                double Sell = Convert.ToDouble(lastNumberString) * x;
                Sell = Math.Round(Sell);

                string newproduct = parts[0] + ",1,-1,1000,-1," + $"{Sell}";
                //Console.WriteLine(trader.Products[i]);
                trader.Products[i] = newproduct;
            }
        }
        public static void rebalance_buy(double x, Tradercategory trader)
        {
            for (int i = 0; i < trader.Products.Count; i++)
            {

                string[] parts = trader.Products[i].Split(',');
                string lastNumberString = parts[4];
                double Sell = Convert.ToDouble(lastNumberString) * x;
                Sell = Math.Round(Sell);

                string newproduct = parts[0] + $",1,-1,1000,{Sell},-1";
                trader.Products[i] = newproduct;
            }
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


        
