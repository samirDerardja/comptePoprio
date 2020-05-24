using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dataApp.API.Controllers
{
[Route("api/owner")] 
    [ApiController] 
    public class OwnerController : ControllerBase 
    { 
        private ILoggerManager _logger; 
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper) 
        { 
            _logger = logger; 
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet] 
        public IActionResult GetAllOwners() 
        { 
            try 
            { 
                var owners = _repository.Owner.GetAllOwners(); 
                _logger.LogInfo($"Returned all owners from database.");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownersResult); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "OwnerById")] 
        public IActionResult GetOwnerById(Guid id) 
        { 
            try 
            { 
                var owner = _repository.Owner.GetOwnerById(id); 
                if (owner == null) 
                { 
                    _logger.LogError($"Le propietaire avec l' id : {id},n' a pas été trouvé dans la base de donnée."); 
                    return NotFound(); 
                } 
                else 
                { 
                    _logger.LogInfo($"Returned owner with id: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}/account")] 
        public IActionResult GetOwnerWithDetails(Guid id) 
        { 
            try 
            { 
                var owner = _repository.Owner.GetOwnerWithDetails(id); 
                if (owner == null) 
                { 
                    _logger.LogError($"Owner with id: {id},n' a pas été trouvé dans la base de donnée."); 
                    return NotFound(); 
                } 
                else 
                { 
                    _logger.LogInfo($"Returned owner with details for id: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPost]

        //*owner de ownercreationdto correspond a notre model qui communique avec la base de donnée
        //*ses attributs doivent correspondrent exactement a ceux des tables en BDD
public IActionResult CreateOwner([FromBody]OwnerForCreationDto owner)
{
    try
    {
        if (owner == null)
        {
            _logger.LogError("Owner object sent from client is null.");
            return BadRequest("Owner object is null");
        }
 //*modelstate va verifier l état d un model specialement creer pour le CREATE, a savoir si il est valid et non null. 
 //*il herite de la classe abstrait controllerBase
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }
 
 //* si tt est ok : on map notre model dto avec le model generique OWNER
        var ownerEntity = _mapper.Map<Owner>(owner);
 
 //* on le fait passé en parametre via l interface irepositorywrapper ( qui est une couche generique ) 
 //* qui elle va contenir une autre sous couche qui fait appel a la couche iownerRepository qui contien les methodes CRUD
        _repository.Owner.CreateOwner(ownerEntity);
        //* on enregistre
        _repository.Save();
 
        var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);
 
        return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
    }
    catch (Exception ex)
    {
        _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}

[HttpPut("{id}")]
public IActionResult UpdateOwner(Guid id, [FromBody]OwnerForUpdateDto owner)
{
    try
    {
        if (owner == null)
        {
            _logger.LogError("Owner object sent from client is null.");
            return BadRequest("Owner object is null");
        }
 
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }
 
        var ownerEntity = _repository.Owner.GetOwnerById(id);
        if (ownerEntity == null)
        {
            _logger.LogError($"Owner with id: {id},n' a pas été trouvé dans la base de donnée.");
            return NotFound();
        }
 
        _mapper.Map(owner, ownerEntity);
 
        _repository.Owner.UpdateOwner(ownerEntity);
        _repository.Save();
 
        return NoContent();
    }
    catch (Exception ex)
    {
        _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}
[HttpDelete("{id}")]
public IActionResult DeleteOwner(Guid id)
{
	try
	{
		var owner = _repository.Owner.GetOwnerById(id);
		if(owner == null)
		{
			_logger.LogError($"Owner with id: {id},n' a pas été trouvé dans la base de donnée.");
			return NotFound();
		}

        
                
    if(_repository.Account.AccountsByOwner(id).Any())
{
    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
}
 
		_repository.Owner.DeleteOwner(owner);
                _repository.Save();

 
		return NoContent();
	}
	catch (Exception ex)
	{
		_logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
		return StatusCode(500, "Internal server error");
	}

}
    }
}