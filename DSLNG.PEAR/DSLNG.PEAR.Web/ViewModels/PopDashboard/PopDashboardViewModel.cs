﻿using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.PopDashboard
{
    public class PopDashboardViewModel
    {
        public PopDashboardViewModel()
        {
            PopDashboards = new List<PopDashboard>();
        }
        public IList<PopDashboard> PopDashboards { get; set; }

        public class PopDashboard
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Number { get; set; }
            public string Subtitle { get; set; }
            public bool IsActive { get; set; }
        }
    }


    public class SavePopDashboardViewModel
    {
        public SavePopDashboardViewModel()
        {
            IsActive = true;
            Statuses = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Subtitle { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }
        public string Attachment { get; set; }
    }

    public class GetPopDashboardViewModel
    {
        public GetPopDashboardViewModel()
        {
            PopInformations = new List<PopInformation>();
            Signatures = new List<SignatureViewModel>();
            IsApprove = false;
            IsReject = false;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Subtitle { get; set; }
        public bool IsActive { get; set; }
        public IList<PopInformation> PopInformations { get; set; }
        public IList<SignatureViewModel> Signatures { get; set; }
        public List<SelectListItem> Users { get; set; }
        public int UserId { get; set; }
        public int DashboardId { get; set; }
        public int TypeSignature { get; set; }
        public bool IsApprove { get; set; }
        public bool IsReject { get; set; }
        public string Attachment { get; set; }
        public string Status { get; set; }

        public class PopInformation
        {
            public int Id { get; set; }
            public PopInformationType Type { get; set; }
            public string Title { get; set; }
            public string Value { get; set; }
        }
    }
}