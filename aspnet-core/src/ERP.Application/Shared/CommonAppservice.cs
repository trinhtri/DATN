using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using ERP.Authorization.Users;
using ERP.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared
{
    public class CommonAppservice : ERPAppServiceBase, ICommonAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Models.RoleProject, long> _projectRoleRepository;
        private readonly IRepository<Models.Project, long> _projectRepository;
        private readonly IRepository<Models.Status, long> _statusRepository;
        private readonly IRepository<Models.IssueType, long> _issueTypeRepository;
        private readonly IRepository<Models.Priority, long> _prioritiesRepository;
        private readonly IRepository<Models.Sprint, long> _sprintRepository;
        private readonly IRepository<Models.Issue, long> _issueRepository;
        private readonly IRepository<Models.Member, long> _memberRepository;


        public CommonAppservice(IRepository<User, long> userRepository,
            IRepository<Models.RoleProject, long> projectRoleRepository,
            IRepository<Models.Project, long> projectRepository,
            IRepository<Models.Status, long> statusRepository,
            IRepository<Models.IssueType, long> issueTypeRepository,
            IRepository<Models.Priority, long> prioritiesRepository,
            IRepository<Models.Sprint, long> sprintRepository,
            IRepository<Models.Issue, long> issueRepository,
            IRepository<Models.Member, long> memberRepository
            )
        {
            _userRepository = userRepository;
            _projectRoleRepository = projectRoleRepository;
            _projectRepository = projectRepository;
            _statusRepository = statusRepository;
            _issueTypeRepository = issueTypeRepository;
            _prioritiesRepository = prioritiesRepository;
            _sprintRepository = sprintRepository;
            _issueRepository = issueRepository;
            _memberRepository = memberRepository;

        }

        public async Task<List<ERPComboboxItem>> GetLookups(string type, int? tenantId = null, int? parentId = null)
        {
            var result = new List<ERPComboboxItem>();

            switch (type)
            {
                case "Member":
                    result = await _userRepository.GetAll()
                        .Where(x => x.TenantId == tenantId || (tenantId == null && x.TenantId == null))
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.UserName })
                        .ToListAsync();
                    break;
                case "MemberOfProject":
                  result =await _memberRepository.GetAll().Include(x=>x.Employee_).Where(x=>x.Project_Id == parentId)
                        .Where(x => x.TenantId == tenantId)
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.Employee_.UserName })
                        .ToListAsync();

                    break;
                case "Project":
                    result = await _projectRepository.GetAll()
                        .Where(x => x.TenantId == tenantId || (tenantId == null && x.TenantId == null))
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.ProjectCode })
                        .ToListAsync();
                    break;
                case "RoleProject":
                    result = await _projectRoleRepository.GetAll()
                        .Where(x => x.TenantId == tenantId || (tenantId == null && x.TenantId == null))
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.RoleName })
                        .ToListAsync();
                    break;
                case "Priorities":
                    result = await _prioritiesRepository.GetAll()
                        .Where(x => x.TenantId == tenantId || (tenantId == null && x.TenantId == null))
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.PriorityName })
                        .ToListAsync();
                    break;

                case "IssueTypes":
                    result = await _issueTypeRepository.GetAll()
                        .Where(x => x.TenantId == tenantId || (tenantId == null && x.TenantId == null))
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.TypeName })
                        .ToListAsync();
                    break;

                case "Sprints":
                    result = _issueRepository.GetAll()
                        .Where(x=>x.Type == 1)
                        .Where(x => x.TenantId == tenantId || tenantId == null)
                        .WhereIf(parentId.HasValue , x=> x.Project_Id == parentId)
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.IssueCode })
                        .ToList();
                    break;
            }
            return result;
        }
    }
}
