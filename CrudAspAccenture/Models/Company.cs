using System.ComponentModel.DataAnnotations;

namespace CrudAspAccenture.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(14, MinimumLength=14)]
        public string Cnpj { get; set; }
        [Required]       
        public string NomeFantasia { get; set; }
        [Required]
        public string Cep { get; set; }
        public string Supliers { get; set; }        

    }
}
