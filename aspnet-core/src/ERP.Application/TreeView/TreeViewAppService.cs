﻿using Abp.Domain.Repositories;
using ERP.TreeView.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Authorization.Users;
using Abp.Collections.Extensions;

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

        public async Task<TreeNode> GetTreeViewIssue(GetTreeInputDto input)
        {
            try { 
         var flatNode = new List<Node>();
            var projects = _projectRepository.GetAll()
                    .Where(x=>x.Status == true)
                    .WhereIf(!string.IsNullOrEmpty(input.Filter), x=>x.ProjectCode.Contains(input.Filter) 
                     || x.ProjectName.Contains(input.Filter))
                    .WhereIf(input.ListProjectId != null ,x=> input.ListProjectId.Any(a => a == x.Id))
                    .Select(x=> new Node
            {
                Assignee_Id = x.Assignee_Id,
                Discription = null,
                Due_Date= x.EndDate,
                Name = x.ProjectCode,
                Priority = 1,
                Id = x.Id + "_Parent",
                ParentId = null,
                Reporter_Id = x.Reporter_Id,
                Estimate = null,
                Status = (x.Status == true) ? Int64.Parse("1") : Int64.Parse("4"),
                Summary = x.ProjectName,
                Type = 1,
                Created= x.StartDate,
            }).OrderBy(x=>x.Name).ToList();
            flatNode.AddRange(projects);
            var sprints = _sprintRepository.GetAll().Select(x => new Node
            {
                Assignee_Id = x.Assignee_Id,
                Created = x.CreationTime,
                Summary = x.Summary,
                Status = x.Status_Id,
                Discription = x.Discription,
                Due_Date = x.Due_Date,
                Id = x.Id + "_Child",
                Name = x.SprintCode,
                ParentId = x.Project_Id + "",
                Reporter_Id = x.Reporter_Id,
                Estimate = x.Estimate
            }).OrderBy(x => x.Name).ToList();
            flatNode.AddRange(sprints);

            var issues = _issueRepository.GetAll()
                    .WhereIf(!string.IsNullOrEmpty(input.Filter), x => x.TaskCode.Contains(input.Filter)
                     || x.Summary.Contains(input.Filter))
                    .WhereIf(input.ListStatusId != null, x => input.ListStatusId.Any(a => a == x.Status_Id))
                    .WhereIf(input.ListTypeId != null, x => input.ListTypeId.Any(a => a == x.Type_Id))
                    .WhereIf(input.ListAssignId != null, x => input.ListAssignId.Any(a => a == x.Assignee_Id))
                .Select(x => new Node { 
            Created = x.CreationTime,
            Estimate = x.Estimate,
            Reporter_Id = x.Reporter_Id,
            Assignee_Id = x.Assignee_Id,
            Discription = x.Discription,
            Due_Date = x.Due_Date,
            Id = x.Id + "_Child",
            Name = x.TaskCode,
            Priority = x.Priority_Id,
            Status = x.Status_Id,
            Summary = x.Summary,
            Type = x.Type_Id,
            ParentId = x.Discription
                }).OrderBy(x => x.Name).ToList();
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
