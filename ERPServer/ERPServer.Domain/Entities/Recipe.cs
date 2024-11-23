using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;
public sealed class Recipe : Entity
{
    public Guid ProductId { get; set; }
    ICollection<RecipeDetail>? RecipeDetails { get; set; }
}