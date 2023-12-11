using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models
{
    public class Team : BaseEntity
    {
        public string Fullname { get; set; }
        public string Image { get; set; }

        public string FB { get; set; }
        public string Insta { get; set; }
        public string Twit { get; set; }
        public string Linkedin { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }

        public  int ProfessionId { get; set; }
        public Profession? Profession { get; set; }
    }
}
