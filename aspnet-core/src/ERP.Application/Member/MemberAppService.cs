using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using ERP.Member.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using ERP.Dto;
using ERP.Member.Exporting;

namespace ERP.Member
{
   public class MemberAppService: ERPAppServiceBase, IMemberAppService
    {
        private readonly IRepository<Models.Member,long> _memberRepository;
        private readonly IRepository<Models.Project,long> _projectRepository;
        private readonly MemberListExcelExporter _memberListExcelExporter;
        public MemberAppService(IRepository<Models.Member,long> memberRepository,
            IRepository<Models.Project,long> projectRepository,
            MemberListExcelExporter memberListExcelExporter
            )
        {
            _memberRepository = memberRepository;
            _projectRepository = projectRepository;
            _memberListExcelExporter = memberListExcelExporter;
        }

        public async Task<long> Create(CreateMemberDto input)
        {
            input.TenantId = AbpSession.TenantId;
            var dto = ObjectMapper.Map<Models.Member>(input);
            await _memberRepository.InsertAsync(dto);
            return dto.Id;
        }

        public async Task Delete(long id)
        {
            await _memberRepository.DeleteAsync(id);
        }

        public async Task<CreateMemberDto> GetId(long id)
        {
            var dto = await _memberRepository.FirstOrDefaultAsync(id);
            return ObjectMapper.Map<CreateMemberDto>(dto);
        }

        public async Task<PagedResultDto<MemberListDto>> GetAll(MemberInputDto input)
        {
            var list = _projectRepository.GetAll().WhereIf(!input.Filter.IsNullOrWhiteSpace(),
               x => x.ProjectCode.ToUpper().Contains(input.Filter.ToUpper())
               || x.ProjectName.ToUpper().Contains(input.Filter.ToUpper())
               || x.StartDate.ToString().Contains(input.Filter));
            try
            {
                var members = (from m in _memberRepository.GetAll().Include(x => x.Employee_).Include(x=>x.Role_)
                               join p in _projectRepository.GetAll()
                               on m.Project_Id equals p.Id
                               select new MemberListDto
                               {
                                   EffectiveDate = m.EffectiveDate,
                                   EmployeeName = m.Employee_.FullName,
                                   Id = m.Id,
                                   EndDate = m.EndDate,
                                   Note = m.Note,
                                   Role = m.Role_.RoleName,
                                   Project_Id = m.Project_Id,
                               }).Where(x => x.Project_Id == input.Project_Id)
                       .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                        x => x.EmployeeName.ToUpper().Contains(input.Filter.ToUpper())
                       || x.Note.ToUpper().Contains(input.Filter.ToUpper())
                       || x.EffectiveDate.ToString().Contains(input.Filter)
                       || x.EndDate.ToString().Contains(input.Filter));

                var tatolCount = await members.AsQueryable().CountAsync();
                var result = await members.AsQueryable().OrderBy(input.Sorting).PageBy(input).ToListAsync();

                var projectListDtos = ObjectMapper.Map<List<MemberListDto>>(result);

                return new PagedResultDto<MemberListDto>(
                    tatolCount,
                    projectListDtos
                    );
            } catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task Update(CreateMemberDto input)
        {
            var member = await _memberRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, member);
        }

        public async Task<FileDto> GetMemberForExcel(MemberInputDto input)
        {
            var list = await GetAll(input);
            var dto = list.Items.ToList();
            return _memberListExcelExporter.ExportToFile(dto);
        }
    }
}
