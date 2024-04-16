using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Models.DTO_s;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(String orderBy)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal";
        var reader = command.ExecuteReader();
        List<Animal> animals = new List<Animal>();
        while (reader.Read())
        {
            animals.Add(new Animal(){
                Id = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Area = reader.GetString(reader.GetOrdinal("Area")),
                Category = reader.GetString(reader.GetOrdinal("Category"))
                });
        }
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Inser Into Animals VALUES ({@animalName}, {@animalDesc}, {@animalCat}, {@animalArea})";
        command.Parameters.AddWithValue("@animalName", addAnimal.Name);
        command.Parameters.AddWithValue("@animalDesc", addAnimal.Description);
        command.Parameters.AddWithValue("@animalCat", addAnimal.Category);
        command.Parameters.AddWithValue("@animalArea", addAnimal.Area);

        return Created();
    }
    
}
