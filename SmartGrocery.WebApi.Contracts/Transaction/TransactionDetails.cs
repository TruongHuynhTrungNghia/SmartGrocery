using SmartGrocery.WebApi.Contracts.BaseProduct;

namespace SmartGrocery.WebApi.Contracts.Transaction
{
    public class TransactionDetails : Transaction
    {
        public string CustomerName { get; set; }

        public ProductSnapshotContract[] ProductSnapshots { get; set; }
    }
}