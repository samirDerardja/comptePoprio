using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("compte")] 
    public class Account 
    {

        [Column("idCompte")]
        public Guid id { get; set; }

        [Required(ErrorMessage = "Date created is required")] 
        public DateTime DateDeCreation { get; set; }

        [Required(ErrorMessage = "Account type is required")] 
        public string TypeDeCompte { get; set; }

        [ForeignKey(nameof(Owner))]
        public Guid idProprietaire { get; set; }
        public Owner Owner { get; set; }
    }
}
