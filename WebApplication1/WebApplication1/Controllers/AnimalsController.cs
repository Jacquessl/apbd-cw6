using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;
using WebApplication1.Models.DTO_s;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    [HttpGet]
    public IActionResult GetAnimals(String? orderBy)
    {
        if (orderBy.IsNullOrEmpty())
        {
            return Ok(_animalRepository.GetAnimals());
        }
        return Ok(_animalRepository.GetAnimals(orderBy));
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        _animalRepository.AddAnimal(addAnimal);
        return Created();
    }

    [HttpPut]
    public IActionResult ChangeAnimal(AddAnimal addAnimal, int idAnimal)
    {
        _animalRepository.ChangeAnimal(addAnimal, idAnimal);
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        _animalRepository.DeleteAnimal(idAnimal);
        return Ok();
    }
}
