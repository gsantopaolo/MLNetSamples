using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;

namespace BikeSharing
{
    class Program
    {
        static void Main(string[] args)
        {
            string trainDataPath = "train.csv";
            string testDataPath = "test.csv";

            var pipeline = new LearningPipeline();
            pipeline.Add(new TextLoader(trainDataPath).CreateFrom<BikeData>(useHeader: true, separator: ','));
            pipeline.Add(new ColumnCopier(("Count", "Label")));
            pipeline.Add(new ColumnConcatenator("Features","Season","Year","Month","Hour","Weekday","Weather","Temperature","NormalizedTemperature","Humidity","Windspeed"));
            pipeline.Add(new FastTreeRegressor());
            var model = pipeline.Train<BikeData, BikePrediction>();
            var testData = new TextLoader(testDataPath).CreateFrom<BikeData>(useHeader: true, separator: ',');
            var metrics = new RegressionEvaluator().Evaluate(model, testData);

            Console.ReadLine();
        }
    }
}
