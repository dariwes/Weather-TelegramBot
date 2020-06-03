using System.Collections.Generic;
using GoogleChartSharp;

namespace WeatherBot.Commands
{
    class ChartCommand
    {
        public static string Name => "/chart";

        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!Name.Equals(e?.Message?.Text))
            {
                return;
            }

            var user = UserExistCommand.GetUser(e.Message.From.Id);
            var forecast = GetLocationCommand.GetForecast(user.Id);

            if (forecast is null)
            {
                GetLocationCommand.SetLocation(user.Id);
                return;
            }

            var temp = new int[5];
            var marks = new string[5];
            var j = 1;

            for (var i = 0; i < 5; i++, j += 8)
            {
                temp[i] = (int)forecast.List[j].Main.Temp + 5;
                marks[i] = forecast.List[j].Dt_txt;
            }

            var dataset = new List<int[]>();
            dataset.Add(temp);

            var lineChart = new LineChart(600, 300);
            lineChart.SetTitle("График температуры на 5 дней", "0088A0", 14);
            lineChart.SetData(dataset);
            lineChart.SetGrid(10, 10);

            lineChart.AddAxis(new ChartAxis(ChartAxisType.Bottom, marks));
            var chartAxisLeft = new ChartAxis(ChartAxisType.Left);
            chartAxisLeft.SetRange(-20, 80);
            lineChart.AddAxis(chartAxisLeft);

            lineChart.SetDatasetColors(new string[] { "85E0F0" });
            lineChart.SetLegend(new string[] { "Температура" });

            await Bot.client.SendPhotoAsync(user.Id, lineChart.GetUrl());
        }
    }
}
