using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeSharing
{
    // IrisData is used to provide training data, and as 
    // input for prediction operations
    // - First 4 properties are inputs/features used to predict the label
    // - Label is what you are predicting, and is only set when training
    public class BikeData
    {
        [Column("2")]
        public float Season;

        [Column("3")]
        public float Year;

        [Column("4")]
        public float Month;

        [Column("5")]
        public float Hour;

        [Column("6")]
        public bool Holiday;

        [Column("7")]
        public float Weekday;

        [Column("8")]
        public float Weather;

        [Column("9")]
        public float Temperature;

        [Column("10")]
        public float NormalizedTemperature;

        [Column("11")]
        public float Humidity;

        [Column("12")]
        public float Windspeed;

        [Column("16")]
        public float Count;

    }
}
