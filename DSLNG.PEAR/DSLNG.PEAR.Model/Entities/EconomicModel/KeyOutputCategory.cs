﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Data.Entities.EconomicModel
{
    public class KeyOutputCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Order { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public ICollection<KeyOutputConfiguration> KeyOutputs { get; set; }
    }
}
