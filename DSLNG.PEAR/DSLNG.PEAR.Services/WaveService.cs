﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Wave;
using DSLNG.PEAR.Services.Responses;
using DSLNG.PEAR.Services.Responses.Wave;
using DSLNG.PEAR.Services.Responses.Weather;

namespace DSLNG.PEAR.Services
{
    public class WaveService : BaseService, IWaveService
    {
        public WaveService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetWavesResponse GetWaves(GetWavesRequest request)
        {
            var query = DataContext.Waves.AsQueryable();
            if (request.OnlyCount)
            {
                return new GetWavesResponse { Count = query.Count() };
            }
            else
            {
                query = query.Include(x => x.Value);
                query = query.OrderByDescending(x => x.Id).Skip(request.Skip).Take(request.Take);
                return new GetWavesResponse
                {
                    Waves = query.MapTo<GetWavesResponse.WaveResponse>()
                };
            }
        }

        public GetWavesResponse GetWavesForGrid(GetWavesRequest request)
        {
            int totalRecords;
            var data = SortData(request.Search, request.SortingDictionary, out totalRecords);
            if (request.Take != -1)
            {
                data = data.Skip(request.Skip).Take(request.Take);
            }

            return new GetWavesResponse
            {
                TotalRecords = totalRecords,
                Waves = data.ToList().MapTo<GetWavesResponse.WaveResponse>()
            };
        }

        public BaseResponse SaveWave(SaveWaveRequest request)
        {
            try
            {
                if (request.Id != 0)
                {
                    var wave = DataContext.Waves.First(x => x.Id == request.Id);
                    request.MapPropertiesToInstance<Wave>(wave);
                    var value = new SelectOption { Id = request.ValueId };
                    DataContext.SelectOptions.Attach(value);
                    wave.Value = value;
                }
                else
                {
                    var wave = request.MapTo<Wave>();
                    var value = new SelectOption { Id = request.ValueId };
                    DataContext.SelectOptions.Attach(value);
                    wave.Value = value;
                    DataContext.Waves.Add(wave);
                }
                DataContext.SaveChanges();
                return new BaseResponse
                {
                    IsSuccess = true,
                    Message = "Wave data has been saved successfully"
                };
            }
            catch (InvalidOperationException e)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "An error occured while trying to save weather data"
                };
            }
        }

        public GetWaveResponse GetWave(GetWaveRequest request)
        {
            if (request.Date.HasValue)
            {
                var wave = DataContext.Waves.Include(x => x.Value).FirstOrDefault(x => x.Date == request.Date.Value);
                if (wave != null)
                {
                    var resp = wave.MapTo<GetWaveResponse>();
                    resp.ValueId = wave.Value.Id;
                    return resp;
                }
                return new GetWaveResponse();
            }
            else
            {
                GetWaveResponse resp;
                if (request.ByDate)
                {
                    var weather = DataContext.Waves.Include(x => x.Value).OrderByDescending(x => x.Date).FirstOrDefault();
                    if (weather != null)
                    {
                        resp = weather.MapTo<GetWaveResponse>();
                        resp.ValueId = weather.Value.Id;
                        return resp;
                    }
                    return new GetWaveResponse();
                }

                var wave = DataContext.Waves.Include(x => x.Value).FirstOrDefault(x => x.Id == request.Id);
                resp = wave.MapTo<GetWaveResponse>();
                if (wave != null) resp.ValueId = wave.Value.Id;
                return resp;
            }
        }

        public IEnumerable<Wave> SortData(string search, IDictionary<string, SortOrder> sortingDictionary, out int totalRecords)
        {
            var data = DataContext.Waves.Include(x => x.Value).AsQueryable();
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x => x.Value.Value.Contains(search));
            }

            foreach (var sortOrder in sortingDictionary)
            {
                switch (sortOrder.Key)
                {
                    case "PeriodeType":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.PeriodeType)
                            : data.OrderByDescending(x => x.PeriodeType);
                        break;
                    case "Date":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Date)
                            : data.OrderByDescending(x => x.Date);
                        break;
                    case "Value":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Value)
                            : data.OrderByDescending(x => x.Value);
                        break;

                    case "Tide":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Tide)
                            : data.OrderByDescending(x => x.Tide);
                        break;
                }
            }

            totalRecords = data.Count();
            return data;
        }
    }
}
