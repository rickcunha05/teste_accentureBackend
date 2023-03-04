using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CrudAspAccenture.Models
{
    public class Suppliers
    {
        [Key]        
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Cnpj { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Rg { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Cep { get; set; }
        
        public string Seguimento { get; set; }
        
    }
}
