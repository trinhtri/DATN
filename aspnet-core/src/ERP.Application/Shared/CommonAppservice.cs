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
        public CommonAppservice(IRepository<User, long> userRepository,
            IRepository<Models.RoleProject, long> projectRoleRepository,
            IRepository<Models.Project, long> projectRepository)
        {
            _userRepository = userRepository;
            _projectRoleRepository = projectRoleRepository;
            _projectRepository = projectRepository;
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
            }
            return result;
        }
    }
}
