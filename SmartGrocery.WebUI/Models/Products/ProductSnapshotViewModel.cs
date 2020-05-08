namespace SmartGrocery.WebUI.Models.Products
{
    public class ProductSnapshotViewModel
    {
        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public int NumberOfSoldProduct { get; set; }

        public decimal Price { get; set; }

        public int TotalProduct { get; set; }

        public decimal DisplayPrice { get; set; }

        internal void UpdateProductPriceByQuantity()
        {
            this.Price *= this.NumberOfSoldProduct;
        }
    }
}