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
    [Route("api/account")] 
    [ApiController]  
    public class AccountController : ControllerBase
    {
        
        private ILoggerManager _logger; 
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

     [HttpGet] 
        public IActionResult GetAllAccounts() 
        {
        try 
            { 
                var accounts = _repository.Account.GetAllAccounts();
                _logger.LogInfo($"Returned all accounts from database.");

                var accountsResult = _mapper.Map<IEnumerable<AccountDto>>(accounts);
                return Ok(accountsResult); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 



        [HttpGet("{id}", Name = "AccountById")] 
        public IActionResult GetAccountById(Guid id) 
        {  
            try 
            { 
                var acc = _repository.Account.GetAccountById(id); 
                if (acc == null) 
                { 
                    _logger.LogError($"Le propietaire avec l' id : {id},n' a pas été trouvé dans la base de donnée."); 
                    return NotFound(); 
                } 
                else 
                {  
                    _logger.LogInfo($"Returned owner with id: {id}");

                    var accResult = _mapper.Map<AccountDto>(acc);
                    return Ok(accResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        }

[HttpPost]
        public IActionResult CreateAccount([FromBody]AccountForCreationDto account)
{
    try
    {
        if (account == null)
        {
            _logger.LogError("Object compte envoyé au client est null.");
            return BadRequest("Owner object is null");
        }
 //*modelstate va verifier l état d un model specialement creer pour le CREATE, a savoir si il est valid et non null. 
 //*il herite de la classe abstrait controllerBase
        if (!ModelState.IsValid)
        {
            _logger.LogError("Object compte envoyé au client n' est pas valide.");
            return BadRequest("Invalid model object");
        }
 
 //* si tt est ok : on map notre model dto avec le model generique OWNER
        var accEntity = _mapper.Map<Account>(account);
 
 //* on le fait passé en parametre via l interface irepositorywrapper ( qui est une couche generique ) 
 //* qui elle va contenir une autre sous couche qui fait appel a la couche iownerRepository qui contien les methodes CRUD
        _repository.Account.CreateAccount(accEntity);
        //* on enregistre 
        _repository.Save();
 
        var createdAccount = _mapper.Map<AccountDto>(accEntity);
 
        return CreatedAtRoute("AccountById", new { id = createdAccount.Id }, createdAccount);
    }
    catch (Exception ex)
    {
        _logger.LogError($"une erreur c' est produite lors de la creation d' un compte action: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}

[HttpPut("{id}")]
public IActionResult UpdateAccount(Guid id, [FromBody]AccountForUpdateDto account)
{
    try
    {
        if (account == null)
        {
            _logger.LogError("Owner object sent from client is null.");
            return BadRequest("Owner object is null");
        }
 
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid owner object sent from client.");
            return BadRequest("Invalid model object");
        }
 
        var accEntity = _repository.Account.GetAccountById(id);
        if (accEntity == null)
        {
            _logger.LogError($"Owner with id: {id},n' a pas été trouvé dans la base de donnée.");
            return NotFound();
        }
 
        _mapper.Map(account, accEntity);
 
        _repository.Account.UpdateAccount(accEntity);
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
public IActionResult DeleteAccount(Guid id)
{
	try
	{
		var account = _repository.Account.GetAccountById(id);
		if(account == null)
		{
			_logger.LogError($"Owner with id: {id},n' a pas été trouvé dans la base de donnée.");
			return NotFound();
		}

                
    if(_repository.Account.AccountsByOwner(id).Any())
{
    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
}
 
		_repository.Account.DeleteAccount(account);
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