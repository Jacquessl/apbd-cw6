namespace WebApplication1.Models;

public class Animal
{
    public Animal(int id, string name, string category, string area)
    {
        Id = id;
        Name = name;
        Category = category;
        Area = area;
    }

    public Animal()
    {
        
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }
}