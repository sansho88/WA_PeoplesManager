using Microsoft.AspNetCore.Mvc;
using WA_PeoplesManager.Models;
using WA_PeoplesManager.Services;
using Swashbuckle.AspNetCore.Annotations; 

namespace WA_PeoplesManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController(PeopleService peopleService, ILogger<PeopleController> logger) : ControllerBase{
    [HttpPost]
    [SwaggerOperation(Summary = "Créer une nouvelle personne", Description = "Ajoute une nouvelle personne au système.")]
    public IActionResult CreatePeople([FromBody] People people)
    {
        logger.LogInformation("People created: {$0}", people);
        peopleService.CreatePeople(people);
        return Ok();
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Récupérer toutes les personnes", Description = "Retourne la liste de toutes les personnes.")]
    public ActionResult<List<People>> GetAllPeople()
    {
        Console.WriteLine("Hello");
        return Ok(peopleService.GetAllPeople());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Récupérer une personne par ID", Description = "Retourne une personne spécifique en fonction de son ID.")]
    public IActionResult GetPeopleById(int id)
    {
        try
        {
            return Ok(peopleService.GetPeopleById(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Mettre à jour une personne", Description = "Met à jour les informations d'une personne existante.")]
    public IActionResult UpdatePeople(int id, [FromBody] People updatedPeople)
    {
        if (!peopleService.UpdatePeople(id, updatedPeople)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Supprimer une personne", Description = "Supprime une personne en fonction de son ID.")]
    public IActionResult DeletePeople(int id)
    {
        if (!peopleService.DeletePeople(id)) return NotFound();
        return NoContent();
    }

    [HttpPost("{peopleId}/jobs")]
    [SwaggerOperation(Summary = "Ajouter un emploi à une personne", Description = "Ajoute un emploi à la liste des emplois d'une personne.")]
    public IActionResult AddJobToPeople(int peopleId, [FromBody] Job job)
    {
        if (!peopleService.AddJobToPeople(peopleId, job)) return NotFound();
        return NoContent();
    }

    [HttpPost("{peopleId}/jobs/bulk")]
    [SwaggerOperation(Summary = "Ajouter plusieurs emplois à une personne", Description = "Ajoute plusieurs emplois à la liste des emplois d'une personne.")]
    public IActionResult AddJobsToPeople(int peopleId, [FromBody] List<Job> jobs)
    {
        if (!peopleService.AddJobsToPeople(peopleId, jobs)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{peopleId}/jobs")]
    [SwaggerOperation(Summary = "Supprimer un emploi d'une personne", Description = "Supprime un emploi spécifique de la liste des emplois d'une personne.")]
    public IActionResult RemoveJobFromPeople(int peopleId, [FromBody] Job job)
    {
        if (!peopleService.RemoveJobFromPeople(peopleId, job)) return NotFound();
        return NoContent();
    }

    [HttpGet("{peopleId}/jobs")]
    [SwaggerOperation(Summary = "Récupérer les emplois d'une personne", Description = "Retourne la liste des emplois d'une personne spécifique.")]
    public IActionResult GetJobsByPeopleId(int peopleId)
    {
        var jobs = peopleService.GetJobsByPeopleId(peopleId);
        if (jobs == null) return NotFound();
        return Ok(jobs);
    }

    [HttpGet("company/{companyName}")]
    [SwaggerOperation(Summary = "Récupérer les personnes par nom d'entreprise", Description = "Retourne une liste de personnes ayant travaillé pour une entreprise spécifique.")]
    public IActionResult GetPeopleByCompanyName(string companyName)
    {
        return Ok(peopleService.GetPeopleByCompanyName(companyName));
    }

    [HttpGet("{peopleId}/jobs/dates")]
    [SwaggerOperation(Summary = "Récupérer les emplois d'une personne par plage de dates", Description = "Retourne les emplois d'une personne correspondant à une plage de dates donnée.")]
    public IActionResult GetJobsByDateRange(int peopleId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var jobs = peopleService.GetJobsByDateRange(peopleId, startDate, endDate);
        if (jobs == null) return NotFound();
        return Ok(jobs);
    }
}
