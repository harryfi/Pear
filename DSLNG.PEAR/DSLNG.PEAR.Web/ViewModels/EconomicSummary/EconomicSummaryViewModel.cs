﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.EconomicSummary
{
    public class EconomicSummaryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }
    }
}