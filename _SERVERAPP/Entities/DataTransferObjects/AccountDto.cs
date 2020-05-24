using System;
using System.Collections.Generic;
using Entities.Models;

namespace Entities.DataTransferObjects
{
   public class AccountDto 
{
    public Guid Id { get; set; }
    public DateTime DateDeCreation { get; set; }
    public string TypeDeCompte { get; set; }
    public Guid idProprietaire { get; set; }


}
} 