﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.AssumptionData
{
    public class SaveAssumptionDataRequest
    {
        public int Id { get; set; }
        public int IdScenario { get; set; }
        public int IdConfig { get; set; }
        public double ActualValue { get; set; }
        public double ForecastValue { get; set; }
        public string Remark { get; set; }
    }
}