using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Newtonsoft.Json;
using PredictPrice;

namespace PredictPrice.ConsoleProject
{

    class Program
    {
        static void Main(string[] args)
        {
            PredictionModel<HousePrice, HousePricePredicted> model = Train();
            Evaluate(model);
            Console.ReadLine();
        }
        private static void Evaluate(PredictionModel<HousePrice, HousePricePredicted> model)
        {
            var _testdatapath = @"pp-monthly-update-new-version.csv";
            var testData = new TextLoader(_testdatapath).CreateFrom<HousePrice>(useHeader: false, separator: ',');
            var evaluator = new RegressionEvaluator() { LabelColumn = "Price" };
            RegressionMetrics metrics = evaluator.Evaluate(model, testData);
            Console.WriteLine(JsonConvert.SerializeObject(metrics,Formatting.Indented));
        }
        public static PredictionModel<HousePrice, HousePricePredicted> Train()
        {
            string _datapath = @"pp-2018.csv";
            var pipeline = new LearningPipeline();
            var dropper = new ColumnDropper();

            pipeline.Add(new TextLoader(_datapath).CreateFrom<HousePrice>(useHeader: false, separator: ','));
            pipeline.Add(new ColumnDropper() { Column = new string[] { "DateOfTransfer", "Street", "Locality", "PAON", "SAON" } });
            
            pipeline.Add(new CategoricalOneHotVectorizer(
                    "PostCode",
                    "City",
                    "District",
                    "RecordStatus",
                    "Duration",
                    "PropertyType",
                    "OldNew"));

            pipeline.Add(new ColumnConcatenator(
                    "Features",
                    "PostCode",
                    "City",
                    "District",
                    "RecordStatus",
                    "Duration",
                    "PropertyType",
                    "OldNew"));

            pipeline.Add(new FastTreeRegressor() { LabelColumn = "Price" });
            return pipeline.Train<HousePrice, HousePricePredicted>();
        }
    }
}
