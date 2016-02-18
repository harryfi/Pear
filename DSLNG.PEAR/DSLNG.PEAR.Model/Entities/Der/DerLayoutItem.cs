﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Data.Entities.Der
{
    public class DerLayoutItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public DerLayout DerLayout { get; set; }
        public DerArtifact Artifact { get; set; }
    }
}
