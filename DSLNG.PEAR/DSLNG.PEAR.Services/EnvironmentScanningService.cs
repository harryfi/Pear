﻿using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.EnvironmentScanning;
using DSLNG.PEAR.Services.Responses.EnvironmentScanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities.Blueprint;
using DSLNG.PEAR.Services.Responses;

namespace DSLNG.PEAR.Services
{
    public class EnvironmentScanningService : BaseService, IEnvironmentScanningService
    {
        public EnvironmentScanningService(IDataContext dataContext) : base(dataContext) { }


        public GetEnvironmentsScanningResponse GetEnvironmentsScanning(GetEnvironmentsScanningRequest request)
        {
            return DataContext.EnvironmentsScannings.Where(x => x.Id == request.Id)
                .Include(x => x.PlanningBlueprint)
                .Include(x => x.PlanningBlueprint.BusinessPostureIdentification)
                .Include(x => x.ConstructionPhase)
                .Include(x => x.OperationPhase)
                .Include(x => x.ReinventPhase)
                .Include(x => x.Threat)
                .Include(x => x.Opportunity)
                .Include(x => x.Weakness)
                .Include(x => x.Strength)
                .Include(x => x.Constraints)
                .Include(x => x.Constraints.Select(y => y.Relations))
                .Include(x => x.Constraints.Select(y => y.Relations.Select(z => z.ThreatHost)))
                .Include(x => x.Constraints.Select(y => y.Relations.Select(z => z.OpportunityHost)))
                .Include(x => x.Constraints.Select(y => y.Relations.Select(z => z.WeaknessHost)))
                .Include(x => x.Constraints.Select(y => y.Relations.Select(z => z.StrengthHost)))
                .Include(x => x.Challenges)
                .Include(x => x.Challenges.Select(y => y.Relations))
                .Include(x => x.Challenges.Select(y => y.Relations.Select(z => z.ThreatHost)))
                .Include(x => x.Challenges.Select(y => y.Relations.Select(z => z.OpportunityHost)))
                .Include(x => x.Challenges.Select(y => y.Relations.Select(z => z.WeaknessHost)))
                .Include(x => x.Challenges.Select(y => y.Relations.Select(z => z.StrengthHost)))
                .Include(x => x.Constraints.Select(y => y.ESCategory))
                .Include(x => x.Challenges.Select(y => y.ESCategory))
                .FirstOrDefault().MapTo<GetEnvironmentsScanningResponse>();
        }


        public SaveEnvironmentScanningResponse SaveEnvironmentScanning(SaveEnvironmentScanningRequest request)
        {
            if (request.Id == 0)
            {
                if (request.Type == "cp")
                {
                    var Environmen = request.MapTo<UltimateObjectivePoint>();
                    Environmen.ConstructionPhaseHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EsId).FirstOrDefault();
                    DataContext.UltimateObjectivePoints.Add(Environmen);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentScanningResponse
                    {
                        Id = Environmen.Id,
                        Description = Environmen.Description,
                        IsSuccess = true,
                        Message = "Environment has been saved succesfully!"
                    };
                }
                else if (request.Type == "op")
                {
                    var Environmen = request.MapTo<UltimateObjectivePoint>();
                    Environmen.OperationPhaseHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EsId).FirstOrDefault();
                    DataContext.UltimateObjectivePoints.Add(Environmen);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentScanningResponse
                    {
                        Id = Environmen.Id,
                        Description = Environmen.Description,
                        IsSuccess = true,
                        Message = "Environment has been saved succesfully!"
                    };
                }
                else if (request.Type == "rp")
                {
                    var Environmen = request.MapTo<UltimateObjectivePoint>();
                    Environmen.ReinventPhaseHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EsId).FirstOrDefault();
                    DataContext.UltimateObjectivePoints.Add(Environmen);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentScanningResponse
                    {
                        Id = Environmen.Id,
                        Description = Environmen.Description,
                        IsSuccess = true,
                        Message = "Environment has been saved succesfully!"
                    };
                }
                else
                {
                    return new SaveEnvironmentScanningResponse
                    {
                        IsSuccess = false,
                        Message = "False data input!"
                    };
                }
            }
            else
            {
                return new SaveEnvironmentScanningResponse
                {
                    IsSuccess = false,
                    Message = "False data input"
                };
            }

        }


        public DeleteEnvironmentScanningResponse DeleteEnvironmentScanning(DeleteEnvironmentScanningRequest request)
        {

            var ultimate = new UltimateObjectivePoint { Id = request.Id };
            DataContext.UltimateObjectivePoints.Attach(ultimate);
            DataContext.UltimateObjectivePoints.Remove(ultimate);
            DataContext.SaveChanges();

            return new DeleteEnvironmentScanningResponse
            {
                IsSuccess = true,
                Message = "Deleted has been succesfully"
            };
        }




        public DeleteEnvironmentScanningResponse DeleteEnvironmentalScanning(DeleteEnvironmentScanningRequest request)
        {
            var environmental = new EnvironmentalScanning { Id = request.Id };
            DataContext.EnvironmentalScannings.Attach(environmental);
            DataContext.EnvironmentalScannings.Remove(environmental);
            DataContext.SaveChanges();

            return new DeleteEnvironmentScanningResponse
            {
                IsSuccess = true,
                Message = "Deleted has been succesfully"
            };
        }




        public SaveEnvironmentalScanningResponse SaveEnvironmentalScanning(SaveEnvironmentalScanningRequest request)
        {
            if (request.Id == 0)
            {
                if (request.EnviType == "th")
                {
                    var Environmental = request.MapTo<EnvironmentalScanning>();
                    Environmental.ThreatHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EnviId).FirstOrDefault();
                    DataContext.EnvironmentalScannings.Add(Environmental);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentalScanningResponse
                    {
                        Id = Environmental.Id,
                        Description = Environmental.Desc,
                        IsSuccess = true,
                        Message = "Environmental has been saved succesfully"
                    };
                }

                else if (request.EnviType == "opp")
                {
                    var Environmental = request.MapTo<EnvironmentalScanning>();
                    Environmental.OpportunityHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EnviId).FirstOrDefault();
                    DataContext.EnvironmentalScannings.Add(Environmental);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentalScanningResponse
                    {
                        Id = Environmental.Id,
                        Description = Environmental.Desc,
                        IsSuccess = true,
                        Message = "Environmental has been saved succesfully"
                    };
                }

                else if (request.EnviType == "wk")
                {
                    var Environmental = request.MapTo<EnvironmentalScanning>();
                    Environmental.WeaknessHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EnviId).FirstOrDefault();
                    DataContext.EnvironmentalScannings.Add(Environmental);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentalScanningResponse
                    {
                        Id = Environmental.Id,
                        Description = Environmental.Desc,
                        IsSuccess = true,
                        Message = "Environmental has been saved succesfully"
                    };
                }

                else if (request.EnviType == "st")
                {
                    var Environmental = request.MapTo<EnvironmentalScanning>();
                    Environmental.StrengthHost = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EnviId).FirstOrDefault();
                    DataContext.EnvironmentalScannings.Add(Environmental);
                    DataContext.SaveChanges();
                    return new SaveEnvironmentalScanningResponse
                    {
                        Id = Environmental.Id,
                        Description = Environmental.Desc,
                        IsSuccess = true,
                        Message = "Environmental has been saved succesfully"
                    };
                }
                else
                {
                    return new SaveEnvironmentalScanningResponse
                    {
                        IsSuccess = false,
                        Message = "invalid data!"
                    };
                }
            }
            else
            {
                return new SaveEnvironmentalScanningResponse
                {
                    IsSuccess = false,
                    Message = "invalid data!"
                };
            }
        }

        public DeleteConstraintResponse DeleteConstraint(DeleteConstraintRequest request)
        {
            var constraint = new Constraint { Id = request.Id };
            DataContext.Constraint.Attach(constraint);
            DataContext.Constraint.Remove(constraint);
            DataContext.SaveChanges();

            return new DeleteConstraintResponse
            {
                IsSuccess = true,
                Message = "Constraint has been Deleted Successfully"
            };
        }


        public DeleteChallengeResponse DeleteChallenge(DeleteChallengeRequest request)
        {
            var challenge = new Challenge { Id = request.Id };
            DataContext.Challenges.Attach(challenge);
            DataContext.Challenges.Remove(challenge);
            DataContext.SaveChanges();
            return new DeleteChallengeResponse
            {
                IsSuccess = true,
                Message = "Challenge has been Deleted Successfully"
            };
        }



        public SaveConstraintResponse SaveConstraint(SaveConstraintRequest request)
        {
            var constraint = request.MapTo<Constraint>();
            constraint.EnvironmentScanning = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EnviId).FirstOrDefault();
            constraint.ESCategory = DataContext.ESCategories.FirstOrDefault(x => x.Id == request.Category);
            foreach (var id in request.RelationIds)
            {
                 var envistate = DataContext.EnvironmentalScannings.Local.FirstOrDefault(x => x.Id == id);
                 if (envistate == null)
                 {
                     envistate = new EnvironmentalScanning { Id = id };
                     DataContext.EnvironmentalScannings.Attach(envistate);
                 }
                constraint.Relations.Add(envistate);
            }

            DataContext.Constraint.Add(constraint);
            DataContext.SaveChanges();

            var result = DataContext.Constraint
                .Include(x => x.Relations)
                .Include(x => x.ESCategory)
                .Include(x => x.Relations.Select(y => y.ThreatHost))
                .Include(x => x.Relations.Select(y => y.OpportunityHost))
                .Include(x => x.Relations.Select(y => y.WeaknessHost))
                .Include(x => x.Relations.Select(y => y.StrengthHost))
                .Where(x => x.Id == constraint.Id).FirstOrDefault();
            
            return new SaveConstraintResponse
            {
                IsSuccess = true,
                Message = "Constraint has been saved successfully",
                Category = result.ESCategory.Name,
                Definition = result.Definition,
                Id = result.Id,
                Type = result.Type,
                RelationIds = result.Relations.Select(x => x.Id).ToArray(),
                ThreatIds = result.Relations.Where(x => x.ThreatHost != null).Select(y => y.Id).ToArray(),
                OpportunityIds = result.Relations.Where(x => x.OpportunityHost != null).Select(y => y.Id).ToArray(),
                WeaknessIds = result.Relations.Where(x => x.WeaknessHost != null).Select(y => y.Id).ToArray(),
                StrengthIds = result.Relations.Where(x => x.StrengthHost != null).Select(y => y.Id).ToArray(),

            };

        }


        public SaveChallengeResponse SaveChallenge(SaveChallengeRequest request)
        {
            var challenge = request.MapTo<Challenge>();
            challenge.EnvironmentScanning = DataContext.EnvironmentsScannings.Where(x => x.Id == request.EnviId).FirstOrDefault();
            challenge.ESCategory = DataContext.ESCategories.FirstOrDefault(x => x.Id == request.Category);
            foreach(var id in request.RelationIds)
            {
                var envistate = DataContext.EnvironmentalScannings.Local.FirstOrDefault(x => x.Id == id);
                if (envistate == null)
                {
                    envistate = new EnvironmentalScanning { Id = id };
                    DataContext.EnvironmentalScannings.Attach(envistate);
                }
                challenge.Relations.Add(envistate);
            }
            DataContext.Challenges.Add(challenge);
            DataContext.SaveChanges();

            var result = DataContext.Challenges.Where(x => x.Id == challenge.Id)
                .Include(x => x.Relations)
                .Include(x => x.Relations.Select(y => y.ThreatHost))
                .Include(x => x.Relations.Select(y => y.OpportunityHost))
                .Include(x => x.Relations.Select(y => y.WeaknessHost))
                .Include(x => x.Relations.Select(y => y.StrengthHost)).FirstOrDefault();

            return new SaveChallengeResponse
            {
                IsSuccess = true,
                Message = "Challenge has been saved successfully",
                Category = result.ESCategory.Name,
                Definition = result.Definition,
                Id = result.Id,
                Type = result.Type,
                RelationIds = result.Relations.Select(x => x.Id).ToArray(),
                ThreatIds = result.Relations.Where(x => x.ThreatHost != null).Select(y => y.Id).ToArray(),
                OpportunityIds = result.Relations.Where(x => x.OpportunityHost != null).Select(y => y.Id).ToArray(),
                WeaknessIds = result.Relations.Where(x => x.WeaknessHost != null).Select(y => y.Id).ToArray(),
                StrengthIds = result.Relations.Where(x => x.StrengthHost != null).Select(y => y.Id).ToArray(),
            };
        }


        public GetConstraintResponse GetConstraint(GetConstraintRequest request)
        {
            return DataContext.Constraint.Where(x => x.Id == request.Id)
                .Include(x => x.Relations)
                .Include(x => x.Relations.Select(y => y.ThreatHost))
                .Include(x => x.Relations.Select(y => y.OpportunityHost))
                .Include(x => x.Relations.Select(y => y.WeaknessHost))
                .Include(x => x.Relations.Select(y => y.StrengthHost))
                .FirstOrDefault().MapTo<GetConstraintResponse>();
        }


        public GetChallengeResponse GetChallenge(GetChallengeRequest request)
        {
            return DataContext.Challenges.Where(x => x.Id == request.Id)
                .Include(x => x.Relations)
                .Include(x => x.Relations.Select(y => y.ThreatHost))
                .Include(x => x.Relations.Select(y => y.OpportunityHost))
                .Include(x => x.Relations.Select(y => y.WeaknessHost))
                .Include(x => x.Relations.Select(y => y.StrengthHost))
                .FirstOrDefault().MapTo<GetChallengeResponse>();
        }


        public SubmitEnvironmentsScanningResponse SubmitEnvironmentsScanning(int id)
        {
            try
            {
                var environmentsScanning = DataContext.EnvironmentsScannings.Include(x => x.PlanningBlueprint).First(x => x.Id == id);
                environmentsScanning.IsLocked = true;
                var businessPosture = DataContext.BusinessPostures.First(x => x.PlanningBlueprint.Id == environmentsScanning.PlanningBlueprint.Id);
                businessPosture.IsLocked = false;
                DataContext.SaveChanges();
                return new SubmitEnvironmentsScanningResponse
                {
                    IsSuccess = true,
                    Message = "Environments Scanning has been successfully submited",
                    BusinessPostureId = businessPosture.Id
                };
            }
            catch {
                return new SubmitEnvironmentsScanningResponse
                {
                    IsSuccess = false,
                    Message = "An error occured, please contact adminstrator for further information"
                };
            }
        }
    }
}