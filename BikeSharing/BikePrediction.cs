using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeSharing
{
    // IrisPrediction is the result returned from prediction operations
    public class BikePrediction
    {
        [ColumnName("Score")]
        public float PredictedCount;
    }
}
