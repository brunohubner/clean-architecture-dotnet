using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact]
    public void CreateCategory_WithValdParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category name");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category name");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Category(1, "");
        action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Category(1, null);
        action.Should().Throw<DomainExceptionValidation>();
    }
}