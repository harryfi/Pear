﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Web.ViewModels.CalculatorConstant;
using DSLNG.PEAR.Web.ViewModels.ConstantUsage;

namespace DSLNG.PEAR.Web.ViewModels.Calculator
{
    public class PricingCalculatorViewModel
    {
        public PricingCalculatorViewModel()
        {
            Units = new List<SelectListItem>
                {
                    new SelectListItem {Text = "USD/bbl", Value = "usd/bbl"}
                };
        }

        public double JccPrice { get; set; }
        public string Unit { get; set; }
        public IList<SelectListItem> Units { get; set; }
        public IList<ConstantUsageViewModel> ConstantUsages { get; set; }
        public IList<CalculatorConstantViewModel> FeedGasConstants
        {
            get
            {
                return ConstantUsages.First(x => x.Role == "pricing" && x.Group == "feed-gas")
                    .Constants.MapTo<CalculatorConstantViewModel>();
            }
        }

        public IList<CalculatorConstantViewModel> OtherConstants
        {
            get
            {
                return ConstantUsages.First(x => x.Role == "pricing" && x.Group == "other")
                    .Constants.MapTo<CalculatorConstantViewModel>();
            }
        }

        public IList<CalculatorConstantViewModel> CompositionConstants
        {
            get
            {
                return ConstantUsages.First(x => x.Role == "pricing" && x.Group == "composition")
                    .Constants.MapTo<CalculatorConstantViewModel>();
            }
        }

        public IList<CalculatorConstantViewModel> CdsPriceConstants
        {
            get
            {
                return ConstantUsages.First(x => x.Role == "pricing" && x.Group == "cds-price")
                    .Constants.MapTo<CalculatorConstantViewModel>();
            }
        }
    }
}