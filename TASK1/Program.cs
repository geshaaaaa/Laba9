using System;
using System.IO;
using System.Linq;
namespace CSVfile
{
    class Program
    {
        static void Main(string[] args)
        {

            string csvFilePath = @"D:\C#\CSVfile\Laba.csv";
            string resultFilePath = @"D:\\C#\\CSVfile\\labaresult.csv";


            Func<string, DateTime> getDate = line => DateTime.ParseExact(line.Split(';')[0], "dd.MM.yyyy", null);
            Func<string, double> getAmount = line => double.Parse(line.Split(';')[1]);


            Action<DateTime, double> displayDailyTotal = (date, total) => Console.WriteLine($"{date.ToString("dd.MM.yyyy")}: {total}");

            var transactionsByDate = File.ReadAllLines(csvFilePath)
                .GroupBy(getDate);
            foreach (var group in transactionsByDate)
            {
                var date = group.Key;
                var total = group.Sum(getAmount);
                displayDailyTotal(date, total);

            }






            using (var writer = new StreamWriter(resultFilePath))
            {
                foreach (var dailyTotal in transactionsByDate)
                {
                    var date = dailyTotal.Key;
                    var total = dailyTotal.Sum(getAmount);

                    var dateStr = date.ToString("dd.MM.yyyy");
                    writer.WriteLine($"{dateStr},{total}");


                }
            }

            Console.ReadLine();
        }
    }
}