using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class OwnerDto
    {
    public Guid Id { get; set; }
    public string Nom { get; set; }
    public DateTime DateDeNaissance { get; set; }
    public string Adresse { get; set; }

     public IEnumerable<AccountDto> Accounts { get; set; }
    }
}