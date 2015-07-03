﻿

using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Responses.Artifact;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IArtifactService
    {
        //GetSeriesResponse GetSeries(GetSeriesRequest request);
        GetChartDataResponse GetChartData(GetChartDataRequest request);
        GetSpeedometerChartDataResponse GetSpeedometerChartData(GetSpeedometerChartDataRequest request);
        CreateArtifactResponse Create(CreateArtifactRequest request);
        GetArtifactsResponse GetArtifacts(GetArtifactsRequest request);
        GetArtifactResponse GetArtifact(GetArtifactRequest request);
    }
}