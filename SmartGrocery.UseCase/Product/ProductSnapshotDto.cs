namespace SmartGrocery.UseCase.Product
{
    public class ProductSnapshotDto
    {
        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public int NumberOfSoldProduct { get; set; }

        public decimal Price { get; set; }
    }
}