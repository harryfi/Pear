﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace DSLNG.PEAR.Web.ViewModels.NLS
{
    public class NLSViewModel
    {
        public int Id { get; set; }
        public int VesselScheduleId { get; set; }
        public string VesselName { get; set; }
        public DateTime? CreatedAt { get; set; }
        private string _createdAtDisplay { get; set; }
        [Required]
        [Display(Name = "Created At")]
        public string CreatedAtDisplay
        {
            get
            {
                if (CreatedAt.HasValue)
                {
                    return CreatedAt.Value.ToString("MM/dd/yyyy");
                }
                return this._createdAtDisplay;
            }
            set
            {
                this.CreatedAt = DateTime.ParseExact(value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                this._createdAtDisplay = value;
            }
        }

        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
    }
}