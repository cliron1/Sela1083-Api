using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("users")]
public class PeopleController : ControllerBase {
    private readonly IPeopleService service;

    public PeopleController(IPeopleService service) {
        this.service = service;
    }

    // GET users
    [HttpGet]
    public ActionResult<List<Person>> GetList() {
        return service.GetAll();
    }

    // GET users/7
    [HttpGet("{id:int:min(1)}")]
    public ActionResult<Person> GetPerson([FromRoute]int id) {
        var ret = service.GetOne(id);
        if (ret == null)
            return NotFound();
        return ret;
    }

    // POST users
    [HttpPost]
    public ActionResult<Person> AddPerson([FromBody] Person model) {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        service.Add(model);
        return CreatedAtAction(nameof(GetPerson), new { id = model.Id }, model);
    }
}
