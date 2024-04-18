using WebApplication1.Models;
using WebApplication1.Models.DTO_s;

namespace WebApplication1.Repositories;

public interface IAnimalRepository
{
    public IEnumerable<Animal> GetAnimals(String orderBy = "name");
    public void AddAnimal(AddAnimal addAnimal);
    public void ChangeAnimal(AddAnimal addAnimal, int idAnimal);
    public void DeleteAnimal(int idAnimal);
}