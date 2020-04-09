using AutoMapper;
using SmartGrocery.Model.Product;
using SmartGrocery.UseCase.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                        .SingleOrDefault(f => f.ProductNumer == product.ProductNumber).NumberOfSoldProduct;

                    product.UpdateQuantity(product.Quantity - numberofSoldProduct);
                }

                context.SaveChanges();
            }
        }

        public UpdateProductValidationResult ValidListOfProductResult(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var allProductNumber = context.Set<BaseProduct>().Select(x => x.ProductNumber).ToList();

            return new UpdateProductValidationResult
            {
                IsValid = GetProductNumbers(productSnapshotDtos).All(x => allProductNumber.Contains(x)),
                InvalidProdcutNumbers = GetProductNumbers(productSnapshotDtos).Where(x => !allProductNumber.Contains(x))
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
                        .SingleOrDefault(f => f.ProductId == product.Id).NumberOfSoldProduct;

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

        private ICollection<Guid> GetProductIds(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var productIds = productSnapshotDtos.Select(x => x.ProductId);
            
            return !productIds.Contains(Guid.Empty) ? productIds.ToList() : new List<Guid>();
        }

        private ICollection<string> GetProductNumbers(IEnumerable<UpdatedProductSnapshot> productSnapshotDtos)
        {
            var productNumbers = productSnapshotDtos.Select(x => x.ProductNumer);

            return !productNumbers.Contains(null) ? productNumbers.ToList() : new List<string>();
        }
    }

    public class UpdateProductValidationResult
    {
        public IEnumerable<string> InvalidProdcutNumbers { get; set; }

        public bool IsValid { get; set; }
    }

    public class UpdatedProductSnapshot
    {
        public string ProductNumer { get; set; }

        public Guid ProductId {get; set;}

        public int NumberOfSoldProduct { get; set; }

        public decimal Price { get; set; }
    }
}