namespace ERP.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Lock = "Pages.Administration.Users.Lock";
        public const string Pages_Administration_Users_UnLock = "Pages.Administration.Users.UnLock";

        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";


        public const string Pages_Project = "Pages.Project";
        public const string Pages_Project_Create = "Pages.Project.Create";
        public const string Pages_Project_Edit = "Pages.Project.Edit";
        public const string Pages_Project_Delete = "Pages.Project.Delete";
        public const string Pages_Project_Manager = "Pages.Project.Manager";

        public const string Pages_Project_Manager_Member = "Pages.Project.Manager.Member";
        public const string Pages_Project_Manager_Member_Create = "Pages.Project.Manager.Member.Create";
        public const string Pages_Project_Manager_Member_Edit = "Pages.Project.Manager.Member.Edit";
        public const string Pages_Project_Manager_Member_Delete = "Pages.Project.Manager.Member.Delete";

        public const string Pages_Project_Manager_Document = "Pages.Project.Manager.Document";
        public const string Pages_Project_Manager_Document_Create = "Pages.Project.Manager.Document.Create";
        public const string Pages_Project_Manager_Document_Edit = "Pages.Project.Manager.Document.Edit";
        public const string Pages_Project_Manager_Document_Delete = "Pages.Project.Manager.Document.Delete";

        // Issue
        public const string Pages_Issue = "Pages.Issue";
        public const string Pages_Issue_Create = "Pages.Issue.Create";
        public const string Pages_Issue_Edit = "Pages.Issue.Edit";
        public const string Pages_Issue_Delete = "Pages.Issue.Delete";
        public const string Pages_Issue_ReOpen = "Pages.Issue.ReOpen";
        public const string Pages_Issue_Close = "Pages.Issue.Close";


        public const string Pages_Sprint = "Pages.Sprint";
        public const string Pages_Sprint_Create = "Pages.Sprint.Create";
        public const string Pages_Sprint_Edit = "Pages.Sprint.Edit";
        public const string Pages_Sprint_Delete = "Pages.Sprint.Delete";


    }
}