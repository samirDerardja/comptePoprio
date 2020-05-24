using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class AccountForCreationDto
{

    
    [Required(ErrorMessage = "Type de compte requis")]
    [StringLength(60, ErrorMessage = "le type e compte ne dois pas dépasser les 60 charachteres")]
    public string TypeDeCompte{ get; set; }
 
    [Required(ErrorMessage = "Date de creation requis")]
    public DateTime DateDeCreation { get; set; }

    public Guid idProprietaire {get; set; }
 
}
}