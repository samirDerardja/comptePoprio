using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dataApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IRepositoryWrapper _repoWrapper;

        public WeatherForecastController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;

        }

      [HttpGet]
    public IEnumerable<string> Get()
    {
        var domesticAccounts = _repoWrapper.Account.FindByCondition(x => x.TypeDeCompte.Equals("Domestic"));
        var owners = _repoWrapper.Owner.FindAll();
 
        return new string[] { "value1", "value2" };
    }
    }
} 
