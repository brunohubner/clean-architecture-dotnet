using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValdParameters_ResultObjectValidState()
    {
        Action action = () =>
            new Product(1, "Name", "Description", 9.99m, 99, "image_url");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
            new Product(-1, "Name", "Description", 9.99m, 99, "image_url");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShotName()
    {
        Action action = () =>
            new Product(1, "Na", "Description", 9.99m, 99, "image_url");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimun 3 characters");
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () =>
            new Product(1, "Name", "Description", 9.99m, 99, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid image url, too long, maximum 250 characters");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () =>
            new Product(1, "Name", "Description", 9.99m, 99, null);
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () =>
            new Product(1, "Name", "Description", 9.99m, 99, null);
        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () =>
            new Product(1, "Name", "Description", 9.99m, 99, "");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_ExceptionDomainNegativeValue()
    {
        Action action = () =>
            new Product(1, "Name", "Description", -9.99m, 99, "image_url");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        Action action = () =>
            new Product(1, "Name", "Description", 9.99m, value, "image_url");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }
}
