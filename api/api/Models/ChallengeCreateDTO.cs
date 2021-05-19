using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ChallengeCreateDTO
    {
        [Required]
        public string PlayerName { get; set; }

        [Required]
        public string SolutionCode { get; set; }
    }
}
