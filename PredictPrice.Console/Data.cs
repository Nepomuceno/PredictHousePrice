using Microsoft.ML.Runtime.Api;
using System;

namespace PredictPrice
{
    public class HousePrice
    {
        [Column("0")]
        public string TID;

        [Column("1")]
        public float Price;

        [Column("2")]
        public string DateOfTransfer;

        [Column("3")]
        public string PostCode;

        [Column("4")]
        public string PropertyType;

        [Column("5")]
        public string OldNew;

        [Column("6")]
        public string Duration;

        [Column("7")]
        public string PAON;
        [Column("8")]
        public string SAON;
        [Column("9")]
        public string Street;
        [Column("10")]
        public string Locality;
        [Column("11")]
        public string City;
        [Column("12")]
        public string District;
        [Column("13")]
        public string RecordStatus;
    }

    public class HousePricePredicted
    {
        [ColumnName("Score")]
        public float PricePaid;
    }
}