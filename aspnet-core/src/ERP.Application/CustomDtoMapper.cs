using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using ERP.Auditing.Dto;
using ERP.Authorization.Accounts.Dto;
using ERP.Authorization.Permissions.Dto;
using ERP.Authorization.Roles;
using ERP.Authorization.Roles.Dto;
using ERP.Authorization.Users;
using ERP.Authorization.Users.Dto;
using ERP.Authorization.Users.Importing.Dto;
using ERP.Authorization.Users.Profile.Dto;
using ERP.Chat;
using ERP.Chat.Dto;
using ERP.Comment.Dto;
using ERP.ConfigView.Dto;
using ERP.Document.Dto;
using ERP.Editions;
using ERP.Editions.Dto;
using ERP.Friendships;
using ERP.Friendships.Cache;
using ERP.Friendships.Dto;
using ERP.HistoryStatusIssue;
using ERP.HistoryStatusIssue.Dto;
using ERP.Issue.Dto;
using ERP.Localization.Dto;
using ERP.Member.Dto;
using ERP.Migrations;
using ERP.MultiTenancy;
using ERP.MultiTenancy.Dto;
using ERP.MultiTenancy.HostDashboard.Dto;
using ERP.MultiTenancy.Payments;
using ERP.MultiTenancy.Payments.Dto;
using ERP.Notifications.Dto;
using ERP.Organizations.Dto;
using ERP.Project.Dto;
using ERP.Sessions.Dto;
using ERP.Sprint.Dto;

namespace ERP
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */

            // project
            configuration.CreateMap<Models.Project, CreateProjectDto>().ReverseMap();
            configuration.CreateMap<Models.Project, ProjectListDto>().ReverseMap();
            // member
            configuration.CreateMap<Models.Member, CreateMemberDto>().ReverseMap();
            configuration.CreateMap<Models.Member, MemberListDto>().ReverseMap();
            // issue
            configuration.CreateMap<Models.Issue, CreateIssueDto>().ReverseMap();
            configuration.CreateMap<Models.Issue, IssueListOfSprintDto>().ReverseMap();
            configuration.CreateMap<Models.Issue, IssueListDto>()
                .ForMember(x=>x.Project_Id , a => a.MapFrom(b=>b.Sprint_.Project_Id))
                .ForMember(x=>x.SprintName , a => a.MapFrom(b=>b.Sprint_.SprintCode + "-" + b.Sprint_.Project_.ProjectCode))
                .ReverseMap();

            // comment
            configuration.CreateMap<Models.Comment, CreateCommentDto>().ReverseMap();
            configuration.CreateMap<Models.Comment, CommentListDto>()
                .ForMember(r=>r.EmployeeName, re =>re.MapFrom(res =>res.Employee_.UserName))

                .ReverseMap();

            // document
            configuration.CreateMap<Models.Document, CreateDocumentDto>().ReverseMap();
            configuration.CreateMap<Models.Document, DocumentListDto>().ReverseMap();

            configuration.CreateMap<Models.ConfigView, ConfigViewDto>().ReverseMap();
            configuration.CreateMap<Models.Sprint, SprintListDto>()
                .ForMember(r=>r.ProjectName, pn=> pn.MapFrom(e => e.Project_.ProjectName))
                .ReverseMap();
            configuration.CreateMap<Models.Sprint, CreateSprintDto>().ReverseMap();

            configuration.CreateMap<Models.HistoryStatusIssue, CreateHistoryStatusIssueDto>().ReverseMap();
            configuration.CreateMap<Models.HistoryStatusIssue, HistoryStatusIssueListDto>()
                .ForMember(u=>u.UserName, us=>us.MapFrom(x=>x.User_.UserName))
                .ReverseMap();


        }
    }
}