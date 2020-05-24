using System;
using System.ComponentModel.DataAnnotations;



//! pour eviter le code dupliqué nous pourrons mettre le code du update et du create
//! dans une classe abstraite a la quelle nous ferons appel
namespace Entities.DataTransferObjects
{
public class OwnerForUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
    public string Nom { get; set; }
 
    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime DateDeNaissance { get; set; }
 
    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address cannot be loner then 100 characters")]
    public string Adresse { get; set; }
}
}