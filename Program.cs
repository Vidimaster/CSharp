using System.Text.Json;
using System;
using System.IO;

namespace Linq
{
    public static class Globals
    {

        public static string _cityDistrict = "";
        public static DateTime _firstDeliveryDateTime = new DateTime(2000, 1, 1, 0, 0, 0);
        public static string _deliveryOrder = Environment.CurrentDirectory + @"\output.txt";
        public static string _deliveryLog = Environment.CurrentDirectory + @"\log.txt";

    }


    internal class Program
    {
        public static void WriteLog(string strLog)
        {

            using (StreamWriter log = File.AppendText(Globals._deliveryLog))
            {
                log.WriteLine(strLog);
            }

        }
        public static void ListOperations(List<Item> source)
        {

            string[] args = Environment.GetCommandLineArgs();
            WriteLog("----- " + DateTime.Now.ToString("hh:mm:ss tt") + "-----");
            try
            {
                for (int i = 1; i < args.Length; i++)
                {

                    if (args[i] == "_cityDistrict")
                    {
                        Globals._cityDistrict = args[i + 1];
                    }
                    else if (args[i] == "_firstDeliveryDateTime")
                    {

                        Globals._firstDeliveryDateTime = DateTime.ParseExact(args[i + 1], "yyyy-MM-dd HH:mm:ss", null).AddMinutes(30);
                    }


                    else if (args[i] == "_deliveryOrder")
                    {
                        Globals._deliveryOrder = args[i + 1];
                        if (!Directory.Exists(Globals._deliveryOrder))
                        {
                            Globals._deliveryOrder = Environment.CurrentDirectory + @"\output.txt";
                        }
                        else
                        {
                            Globals._deliveryOrder = Globals._deliveryOrder + @"\output.txt";
                        }
                    }
                    else if (args[i] == "_deliveryLog")
                    {
                        Globals._deliveryLog = args[i + 1];
                        if (!Directory.Exists(Globals._deliveryLog))
                        {
                            Globals._deliveryLog = Environment.CurrentDirectory + @"\log.txt";
                        }
                        else
                        {
                            Globals._deliveryLog = Globals._deliveryLog + @"\log.txt";
                        }
                    }


                }

                var filteredResults = from item in source
                                      where item.CityDistrict == Globals._cityDistrict && DateTime.ParseExact(item.DeliveryTime, "yyyy-MM-dd HH:mm:ss", null) >= Globals._firstDeliveryDateTime
                                      select item;

                File.WriteAllText(Globals._deliveryOrder, "");
                using (StreamWriter sw = File.AppendText(Globals._deliveryOrder))
                {
                    foreach (var result in filteredResults)
                    {
                        DateTime date = DateTime.ParseExact(result.DeliveryTime, "yyyy-MM-dd HH:mm:ss", null);
                        sw.WriteLine("Id: " + result.Id + " " + "Weight: " + result.Weight + " " + "DeliveryTime: " + date.ToString("yyyy-MM-dd HH:mm:ss") + " " + "City District: " + result.CityDistrict);
                    }
                }
                WriteLog("Result written to: " + Globals._deliveryOrder);
            }
            catch (Exception e)
            {

                WriteLog("Incorrect input: " + Globals._deliveryOrder);
            }

        }
        static void Main(string[] args)
        {
            List<Item> source = new List<Item>();

            using (StreamReader r = new StreamReader(Environment.CurrentDirectory + @"\input.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Item>>(json);
            }

            ListOperations(source);
        }

    }


    public class Item
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public string DeliveryTime { get; set; }
        public string CityDistrict { get; set; }
    }
}