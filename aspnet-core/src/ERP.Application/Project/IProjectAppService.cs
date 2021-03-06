﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Dto;
using ERP.Project.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Project
{
    public interface IProjectAppService : IApplicationService
    {
        Task<PagedResultDto<ProjectListDto>> GetAll(ProjectInputDto input);
        Task<long> Create(CreateProjectDto input);
        Task Delete(long id);
        Task Update(CreateProjectDto input);
        Task<CreateProjectDto> GetId(long id);

        Task<FileDto> GetProjectToExcel(ProjectInputDto inputDto);
    }
}
