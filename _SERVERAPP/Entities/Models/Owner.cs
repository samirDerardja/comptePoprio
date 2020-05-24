using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("proprietaire")] 
    public class Owner 
    { 

         [Column("idProprietaire")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")] 
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")] 
        public string Nom { get; set; }

        [Required(ErrorMessage = "Date of birth is required")] 
        public DateTime DateDeNaissance { get; set; }

        [Required(ErrorMessage = "Address is required")] 
        [StringLength(100, ErrorMessage = "Address cannot be loner then 100 characters")] 
        public string Adresse { get; set; }

 
//*  lICollection car un proprietaire peu avoir 1 ou plusieurs compte
        public ICollection<Account> Accounts { get; set; }
    }
}
