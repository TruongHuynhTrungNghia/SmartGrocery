﻿using SmartGrocery.WebApi.Contracts.BaseProduct;

namespace SmartGrocery.WebApi.Contracts.Transaction
{
    public class TransactionDetails : Transaction
    {
        public string CustomerName { get; set; }

        public string CustomerId { get; set; }

        public string CustomerEmotion { get; set; }

        public string CustomerEmotionProbability { get; set; }

        public ProductSnapshotContract[] ProductSnapshots { get; set; }
    }
}