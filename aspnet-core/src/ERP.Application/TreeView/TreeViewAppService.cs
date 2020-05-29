using Abp.Domain.Repositories;
using ERP.TreeView.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Authorization.Users;

namespace ERP.TreeView
{
    public class TreeViewAppService : ERPAppServiceBase, ITreeViewAppService
    {
        private readonly IRepository<Models.Project, long> _projectRepository;
        private readonly IRepository<Models.Issue, long> _issueRepository;
        private readonly IRepository<Models.Sprint, long> _sprintRepository;
        private readonly IRepository<User, long> _userRepository;

        public TreeViewAppService(IRepository<Models.Project, long> projectRepository,
           IRepository<Models.Issue, long> issueRepository,
           IRepository<Models.Sprint, long> sprintRepository,
           IRepository<User, long> userRepository
            )
        {
            _projectRepository = projectRepository;
            _issueRepository = issueRepository;
            _sprintRepository = sprintRepository;
            _userRepository = userRepository;
        }

        public async Task<TreeNode> GetTreeViewIssue()
        {
            try { 
         var flatNode = new List<Node>();
            var projects = _projectRepository.GetAll().Select(x=> new Node
            {
                Assignee_Id = null,
                Discription = null,
                Due_Date= null,
                Name = x.ProjectCode,
                Priority = null,
                Id = x.Id,
                ParentId = null,
                Reporter_Id = null,
                Estimate = null,
                Status = null,
                Summary = x.ProjectName,
                Type = null,
                Created= null,
            }).ToList();
            flatNode.AddRange(projects);
            var sprints = _sprintRepository.GetAll().Select(x => new Node
            {
                Assignee_Id = null,
                Created = null,
                Type = null,
                Summary = x.Summary,
                Status = null,
                Discription = null,
                Due_Date = null,
                Id = x.Id,
                Name = x.SprintName,
                ParentId = x.Project_Id,
                Priority = null,
                Reporter_Id = null,
                Estimate = null,
            }).ToList();
            flatNode.AddRange(sprints);

            var issues = _issueRepository.GetAll()
                .Select(x => new Node { 
            Created = x.Update_Date,
            Estimate = x.Estimate,
            Reporter_Id = x.Reporter_Id,
            Assignee_Id = x.Assignee_Id,
            Discription = x.Discription,
            Due_Date = x.Due_Date,
            Id = x.Id,
            Name = x.IssueCode,
            ParentId = x.Sprint_Id,
            Priority = x.Priority_Id,
            Status = x.Status_Id,
            Summary = x.Summary,
            Type = x.Type_ID
            }).ToList();
            flatNode.AddRange(issues);
            foreach(var item in flatNode)
                {
                    item.Reporter = GetName(item.Reporter_Id);
                    item.Assignee = GetName(item.Assignee_Id);
                }
            var result = new TreeNode { NodeFlat = flatNode };
            return result;
            } catch(Exception e)
            {
                throw e;
            }
        }
        private string GetName(long? id)
        {
            if (id != null)
            {
                var fakeId =  id  ?? 1;
                var user = _userRepository.FirstOrDefaultAsync(fakeId);
                return user.Result.UserName;
            }
            return null;
      
        }
    }
}
