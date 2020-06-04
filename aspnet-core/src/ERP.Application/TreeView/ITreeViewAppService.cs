using Abp.Application.Services;
using ERP.TreeView.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TreeView
{
    public interface ITreeViewAppService: IApplicationService
    {
        Task<TreeNode> GetTreeViewIssue(GetTreeInputDto input);
    }
}
