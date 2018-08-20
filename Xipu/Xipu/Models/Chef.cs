using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Xipu.Models
{
    public class Chef
    {
        public Chef()
        {
            CreateOn = DateTime.Now;
            
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreateOn { get; set; }

        [MaxLength(64)]
        public string ClientIp { get; set; }

        [MaxLength(64)]
        public string UserName { get; set; }

        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string PhoneNumber { get; set; }

        [MaxLength(20)]
        public string Province { get; set; }

        [MaxLength(20)]
        public string Cuisine { get; set; }

        [MaxLength(20)]
        public string Entrypoint { get; set; }
        
    }
}