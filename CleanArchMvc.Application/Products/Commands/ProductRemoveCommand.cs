using MediatR;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Products.Commands;

public class ProductRemoveCommand : IRequest<Product>
{
    public int Id { get; set; }

    public ProductRemoveCommand(int id)
    {
        Id = id;
    }
}