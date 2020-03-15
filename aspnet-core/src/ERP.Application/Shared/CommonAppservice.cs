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

        public CommonAppservice(IRepository<User, long> userRepository,
            IRepository<Models.RoleProject, long> projectRoleRepository,
            IRepository<Models.Project, long> projectRepository,
            IRepository<Models.Status, long> statusRepository,
            IRepository<Models.IssueType, long> issueTypeRepository,
            IRepository<Models.Priority, long> prioritiesRepository
            )
        {
            _userRepository = userRepository;
            _projectRoleRepository = projectRoleRepository;
            _projectRepository = projectRepository;
            _statusRepository = statusRepository;
            _issueTypeRepository = issueTypeRepository;
            _prioritiesRepository = prioritiesRepository;

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
                case "Status":
                    result = await _statusRepository.GetAll()
                        .Where(x => x.TenantId == tenantId || (tenantId == null && x.TenantId == null))
                        .Select(x => new ERPComboboxItem { Value = x.Id, DisplayText = x.StatusName })
                        .ToListAsync();
                    break;
            }
            return result;
        }
    }
}
