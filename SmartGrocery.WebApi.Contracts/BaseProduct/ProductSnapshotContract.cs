namespace SmartGrocery.WebApi.Contracts.BaseProduct
{
    public class ProductSnapshotContract
    {
        public string ProductNumber { get; set; }

        public int NumberOfSoldProduct { get; set; }

        public decimal Price { get; set; }
    }
}