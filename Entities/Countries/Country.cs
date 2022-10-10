using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Country :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public int IsoNumericCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alpha2Code { get; set; }
        [Required]
        public string Alpha3Code { get; set; }
        [Required] 
        public string ContinentCode { get; set; }   
    }
}
