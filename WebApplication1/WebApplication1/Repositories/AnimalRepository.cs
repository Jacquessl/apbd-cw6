using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Models.DTO_s;

namespace WebApplication1.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy = "name")
    {
        List<Animal> animals = new List<Animal>();
        if (orderBy.ToLower().Equals("idanimal") || orderBy.ToLower().Equals("name") ||
            orderBy.ToLower().Equals("description") || orderBy.ToLower().Equals("area") || orderBy.ToLower().Equals("category"))
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy}";

            var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                animals.Add(new Animal()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Area = reader.GetString(reader.GetOrdinal("Area")),
                    Category = reader.GetString(reader.GetOrdinal("Category"))
                });
            }
        }
        return animals;
    }

    public void AddAnimal(AddAnimal addAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Insert Into Animal VALUES (@animalName, @animalDesc, @animalCat, @animalArea)";
        command.Parameters.AddWithValue("@animalName", addAnimal.Name);
        command.Parameters.AddWithValue("@animalDesc", addAnimal.Description);
        command.Parameters.AddWithValue("@animalCat", addAnimal.Category);
        command.Parameters.AddWithValue("@animalArea", addAnimal.Area);
        command.ExecuteNonQuery();
    }

    public void ChangeAnimal(AddAnimal addAnimal, int idAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name=@animalName, Description=@animalDesc, Category=@animalCat, Area=@animalArea WHERE IdAnimal=@idAnimal";
        command.Parameters.AddWithValue("@animalName", addAnimal.Name);
        command.Parameters.AddWithValue("@animalDesc", addAnimal.Description);
        command.Parameters.AddWithValue("@animalCat", addAnimal.Category);
        command.Parameters.AddWithValue("@animalArea", addAnimal.Area);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        command.ExecuteNonQuery();
    }

    public void DeleteAnimal(int idAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal=@idAnimal";
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.ExecuteNonQuery();
    }
}