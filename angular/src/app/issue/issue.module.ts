import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule, CheckboxModule } from 'primeng/primeng';
import { ProjectServiceProxy, MemberServiceProxy, CommonAppserviceServiceProxy, DocumentServiceProxy, IssueServiceProxy, CommentServiceProxy, ConfigviewServiceProxy, SprintServiceProxy, TreeViewServiceProxy, HistoryStatusIssueServiceProxy } from '@shared/service-proxies/service-proxies';
import {AccordionModule} from 'primeng/accordion';
NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();
import { FileUploadModule } from 'ng2-file-upload';
import { WorkflowManagementComponent } from './workflow-management.component';
import { CreateIssueComponent } from './create-issue/create-issue.component';
import { ManagementIssueComponent } from './management-issue/management-issue.component';
import { CommentsComponent } from './comments/comments.component';
import { CreateOrEditCommentComponent } from './comments/create-or-edit-comment/create-or-edit-comment.component';
import { ConfigViewComponent } from './config-view/config-view.component';
import {InputSwitchModule} from 'primeng/inputswitch';
import {MultiSelectModule} from 'primeng/multiselect';
import { EstimateComponent } from './management-issue/estimate/estimate.component';
import {EditorModule} from 'primeng/editor';
import {TreeTableModule} from 'primeng/treetable';
import {TreeNode} from 'primeng/api';
import { CreateOrUpdateSprintComponent } from './create-or-update-sprint/create-or-update-sprint.component';
import {InputTextareaModule} from 'primeng/inputtextarea';
import { IssueRoutingModule } from './issue-routing.module';
import { IssueComponent } from './issue/issue.component';
import { IssueOfUserComponent } from './issue/issue-of-user/issue-of-user.component';
import { HistoryStatusIssueComponent } from './history-status-issue/history-status-issue.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ModalModule,
        IssueRoutingModule,
        TabsModule,
        TableModule,
        PaginatorModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot(),
        AccordionModule,
        FileUploadModule,
        InputSwitchModule,
        CheckboxModule,
        MultiSelectModule,
        EditorModule,
        TreeTableModule,
        InputTextareaModule
    ],
    declarations: [
        WorkflowManagementComponent,
        CreateIssueComponent,
        ManagementIssueComponent,
        CommentsComponent,
        CreateOrEditCommentComponent,
        ConfigViewComponent,
        EstimateComponent,
        CreateOrUpdateSprintComponent,
        IssueComponent,
        IssueOfUserComponent,
        HistoryStatusIssueComponent,
    ],
    providers: [
        ProjectServiceProxy,
        MemberServiceProxy,
        DocumentServiceProxy,
        IssueServiceProxy,
        CommentServiceProxy,
        CommonAppserviceServiceProxy,
        ConfigviewServiceProxy,
        SprintServiceProxy,
        TreeViewServiceProxy,
        HistoryStatusIssueServiceProxy,
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class IssueModule { }
