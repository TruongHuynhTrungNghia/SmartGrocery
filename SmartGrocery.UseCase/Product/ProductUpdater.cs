using AutoMapper;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartGrocery.UseCase.Product
{
    public class ProductUpdater
    {
        private readonly SmartGroceryContext context;
        private readonly IMapper mapper;

        public ProductUpdater(SmartGroceryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void UpdateProductQuantity(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var changedProducts = GetBaseProductByItsSnapshot(productSnapshotDtos);

            if (changedProducts.Any())
            {
                foreach (var product in changedProducts)
                {
                    var numberofSoldProduct = productSnapshotDtos
                        .SingleOrDefault(f => f.ProductNumber == product.ProductNumber).Amount;

                    product.UpdateQuantity(product.Quantity - numberofSoldProduct);
                }

                context.SaveChanges();
            }
        }

        public UpdateProductValidationResult ValidListOfProductResult(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var allProductNumber = context.Set<BaseProduct>()
                .Where(x => x.Id != null)
                .Select(x => new UpdatedProduct
                {
                    ProductNumber = x.ProductNumber,
                    Amount = x.Quantity
                }).ToList();

            return new UpdateProductValidationResult
            {
                IsValid = CheckValidProduct(productSnapshotDtos, allProductNumber),
                InvalidProdcutNumbers = GetInValidProductNumber(productSnapshotDtos, allProductNumber)
            };
        }

       

        public ICollection<ProductSnapshot> CreateNewListofProductSnapshot(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos, Guid transactionId)
        {
            var productSnapshots = mapper.Map<ProductSnapshot[]>(productSnapshotDtos);
            var baseProduct = GetBaseProductByItsSnapshot(productSnapshotDtos);

            if (!baseProduct.Any())
            {
                throw new ArgumentException("base product should contains id");
            }

            foreach (var product in productSnapshots)
            {
                product.ProductId = baseProduct
                    .SingleOrDefault(x => GetProductNumbers(productSnapshotDtos).Contains(x.ProductNumber)).Id;
                product.TransactionId = transactionId;
            }

            return productSnapshots;
        }

        public void RevertQuantiyOfBaseProduct(IEnumerable<UpdatedProductSnapshot> productSnapshots)
        {
            var baseProducts = GetBaseProductByItsSnapshot(productSnapshots);

            if (baseProducts.Any())
            {
                foreach (var product in baseProducts)
                {
                    var numberofSoldProduct = productSnapshots
                        .SingleOrDefault(f => f.ProductId == product.Id).Amount;

                    product.UpdateQuantity(product.Quantity + numberofSoldProduct);
                }

                context.SaveChanges();
            }
        }

        public void DeleteExistingProductSnapshot(Guid transactionId)
        {
            var productSnapshots = context.Set<ProductSnapshot>()
                .Where(x => x.TransactionId == transactionId).ToList();

            context.Set<ProductSnapshot>().RemoveRange(productSnapshots);

            context.SaveChanges();
        }

        private List<BaseProduct> GetBaseProductByItsSnapshot(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var listOfProductNumbers = GetProductNumbers(productSnapshotDtos);

            if (!listOfProductNumbers.Any())
            {
                var listOfProductId = GetProductIds(productSnapshotDtos);

                if (!listOfProductId.Any())
                {
                    //TODO: Should return empty list
                    return new List<BaseProduct>();
                }

                return context.Set<BaseProduct>()
                    .Where(x => listOfProductId.Contains(x.Id)).ToList();
            }

            return context.Set<BaseProduct>()
                .Where(x => listOfProductNumbers.Contains(x.ProductNumber)).ToList();
        }

        private bool CheckValidProduct(IEnumerable<UpdatedProductSnapshot> updatedProductSnaphots, ICollection<UpdatedProduct> updatedProducts)
        {
            var checkedProductNumbers = GetProductNumbers(updatedProducts);

            return AllUpdatedProductNumberExistInDatabase() && AllUpdatedProductQuantityIsSmallerThanProduct();

            bool AllUpdatedProductNumberExistInDatabase()
                => GetProductNumbers(updatedProductSnaphots).All(x => checkedProductNumbers.Contains(x));

            bool AllUpdatedProductQuantityIsSmallerThanProduct()
            {
                var isValid = true;
                foreach (var snapshot in updatedProductSnaphots)
                {

                    isValid = updatedProducts
                        .FirstOrDefault(x => x.ProductNumber == snapshot.ProductNumber).Amount > snapshot.Amount;
                    if (!isValid)
                        break;
                }

                return isValid;
            }
        }

        private IEnumerable<string> GetInValidProductNumber(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos, List<UpdatedProduct> allProduct)
        {
            //TODO: Refactor this function to return exactly error. 
            var checkedProductNumbers = GetProductNumbers(allProduct);
            var invalidProductNumbers = new List<string>();
                
            var notExistedproductNumber = GetProductNumbers(productSnapshotDtos).Where(x => !checkedProductNumbers.Contains(x));
            var overQuantityproductNumber = GetProductNumbersWasOverQuantity(productSnapshotDtos, allProduct);
            
            invalidProductNumbers.AddRange(notExistedproductNumber);
            invalidProductNumbers.AddRange(overQuantityproductNumber);

            return invalidProductNumbers;
        }

        private IEnumerable<string> GetProductNumbersWasOverQuantity(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos, List<UpdatedProduct> allProduct)
        {
            var invalidProductNumber = new List<string>();

            foreach (var snapshot in productSnapshotDtos)
            {
                if (IsSnapshotAmountGreaterThanItsBase(snapshot))
                {
                    invalidProductNumber.Add(snapshot.ProductNumber);
                }

            }

            return invalidProductNumber;

            bool IsSnapshotAmountGreaterThanItsBase(UpdatedProductSnapshot snapshot)
                => allProduct.FirstOrDefault(x => x.ProductNumber == snapshot.ProductNumber).Amount < snapshot.Amount; 
        }

        private ICollection<Guid> GetProductIds(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var productIds = productSnapshotDtos.Select(x => x.ProductId);

            return !productIds.Contains(Guid.Empty) ? productIds.ToList() : new List<Guid>();
        }

        private ICollection<string> GetProductNumbers(IEnumerable<UpdatedProduct> productSnapshotDtos)
        {
            var productNumbers = productSnapshotDtos.Select(x => x.ProductNumber);

            return !productNumbers.Contains(null) ? productNumbers.ToList() : new List<string>();
        }
    }

    public class UpdateProductValidationResult
    {
        public IEnumerable<string> InvalidProdcutNumbers { get; set; }

        public bool IsValid { get; set; }
    }

    public class UpdatedProductSnapshot : UpdatedProduct
    {
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }
    }

    public class UpdatedProduct
    {
        public string ProductNumber { get; set; }

        public int Amount { get; set; }
    }
}