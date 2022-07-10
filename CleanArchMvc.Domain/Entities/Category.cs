using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }

    public ICollection<Product> Product { get; set; }
    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value");
        ValidateDomain(name);
        Id = id;
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(
            string.IsNullOrEmpty(name),
            "Invalid name. Name is required"
        );

        DomainExceptionValidation.When(
            name.Length < 3,
            "Invalid name, too short, minimun 3 characters"
        );

        Name = name;
    }
}