﻿using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ERP.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            //pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("Create"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("Edit"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("Delete"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            //users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Lock, L("Lock"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_UnLock, L("Unlock"));



            //var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            //var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            //administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            //pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            //administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);

            var projects = pages.CreateChildPermission(AppPermissions.Pages_Project, L("Projects"));
            projects.CreateChildPermission(AppPermissions.Pages_Project_Create, L("Create"));
            projects.CreateChildPermission(AppPermissions.Pages_Project_Edit, L("Edit"));
            projects.CreateChildPermission(AppPermissions.Pages_Project_Delete, L("Delete"));
          var managerProject =  projects.CreateChildPermission(AppPermissions.Pages_Project_Manager, L("ManagerProject"));

            var member = managerProject.CreateChildPermission(AppPermissions.Pages_Project_Manager_Member, L("Members"));
            member.CreateChildPermission(AppPermissions.Pages_Project_Manager_Member_Create, L("Create"));
            member.CreateChildPermission(AppPermissions.Pages_Project_Manager_Member_Edit, L("Edit"));
            member.CreateChildPermission(AppPermissions.Pages_Project_Manager_Member_Delete, L("Delete"));

            var document = managerProject.CreateChildPermission(AppPermissions.Pages_Project_Manager_Document, L("Documents"));
            document.CreateChildPermission(AppPermissions.Pages_Project_Manager_Document_Create, L("Create"));
            document.CreateChildPermission(AppPermissions.Pages_Project_Manager_Document_Edit, L("Edit"));
            document.CreateChildPermission(AppPermissions.Pages_Project_Manager_Document_Delete, L("Delete"));

            var issue = pages.CreateChildPermission(AppPermissions.Pages_Issue, L("Issue"));
            issue.CreateChildPermission(AppPermissions.Pages_Issue_Create, L("Create"));
            issue.CreateChildPermission(AppPermissions.Pages_Issue_Edit, L("Edit"));
            issue.CreateChildPermission(AppPermissions.Pages_Issue_Delete, L("Delete"));
            issue.CreateChildPermission(AppPermissions.Pages_Issue_Compeleted, L("Compeleted")); 
            issue.CreateChildPermission(AppPermissions.Pages_Issue_ReOpened, L("ReOpened"));


            var sprint = pages.CreateChildPermission(AppPermissions.Pages_Sprint, L("Sprint"));
            sprint.CreateChildPermission(AppPermissions.Pages_Sprint_Create, L("Create"));
            sprint.CreateChildPermission(AppPermissions.Pages_Sprint_Edit, L("Edit"));
            sprint.CreateChildPermission(AppPermissions.Pages_Sprint_Delete, L("Delete"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ERPConsts.LocalizationSourceName);
        }
    }
}
