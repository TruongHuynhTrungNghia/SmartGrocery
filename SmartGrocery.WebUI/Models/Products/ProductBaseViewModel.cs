using FluentValidation;
using System;

namespace SmartGrocery.WebUI.Models.Products
{
    public class ProductBaseViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string ProductNumber { get; set; }
    }

    public class ProductBaseViewModelValidator : AbstractValidator<ProductBaseViewModel>
    {
        public ProductBaseViewModelValidator()
        {
            RuleFor(x => x.ProductNumber).NotEmpty();

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Name).NotEmpty();
        }
    }
}