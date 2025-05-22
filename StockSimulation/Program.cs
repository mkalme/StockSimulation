using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockSimulation
{
    class Program
    {
        static double startingValue = 100;
        static double movingByValue = 0.3;
        static int averageTrendLength = 10;
        static int averageSmallTrendLength = 100;

        static int numberOfValues = 20000;

        static double[] allValues = new double[numberOfValues];

        [STAThread]
        static void Main(string[] args)
        {
            allValues[0] = startingValue;

            Random rand = new Random();

            int percentByPositiveTrend = 0;

            int trend = 1; //if number is positive than trend is positive and vice versa
            int percentage = 10; //by how much percentage the value is likely to fallow the trend

            int tempPercent = percentage;
            int variationsByPercent = 0;
            tempPercent -= variationsByPercent / 2;

            int[,] range = { {0, 99}, {100, 199 + tempPercent} }; // second value is the percentage

            int trendLength = rand.Next(averageTrendLength) + (int)(averageSmallTrendLength * 0.5);
            if (rand.Next(2) == 0)
            {
                trend = -1;
            }else {
                trend = 1;

                tempPercent += percentByPositiveTrend;
            }

            int tempTrend = 0;

            string text = allValues[0].ToString() + "\n";

            for (int i = 1; i < numberOfValues; i++) {
                int randomValue = rand.Next((range[1, 1]) + (rand.Next(variationsByPercent + 1)) + 1);

                if (randomValue >= range[1, 0])
                {
                    allValues[i] = allValues[i - 1] + (movingByValue * trend);
                }else {
                    allValues[i] = allValues[i - 1] + (movingByValue * (trend * -1));
                }

                //trend
                tempTrend++;
                if (tempTrend > trendLength) {
                    tempTrend = 0;

                    trendLength = rand.Next(averageTrendLength) + (int)(averageSmallTrendLength * 0.5);
                    if (rand.Next(2) == 0)
                    {
                        trend = -1;
                        tempPercent = percentage - variationsByPercent / 2;

                        range[1, 1] = 199 + tempPercent;
                    } else
                    {
                        trend = 1;
                        tempPercent = percentage - variationsByPercent / 2;
                        tempPercent += percentByPositiveTrend;

                        range[1, 1] = 199 + tempPercent;
                    }
                }

                if (i % 5 == 0) {
                    text += allValues[i] + "\n";
                }
            }

            Clipboard.SetText(text);
            Console.WriteLine("Done");

            Console.ReadLine();
        }
    }
}
