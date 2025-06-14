namespace Domain.Filter;

public class CategoryFilter : ValidFilter
{
    public string? Name { get; set; }

    public CategoryFilter(string Name)
    {
        this.Name = Name;
    }
}
