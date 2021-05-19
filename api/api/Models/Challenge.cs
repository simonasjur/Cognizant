using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Challenge
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string PlayerName { get; set; }

        [Required]
        public string SolutionCode { get; set; }

        [Required]
        public string Output { get; set; }
    }
}
