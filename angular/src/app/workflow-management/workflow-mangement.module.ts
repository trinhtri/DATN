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
import { ProjectServiceProxy, MemberServiceProxy, CommonAppserviceServiceProxy, DocumentServiceProxy, IssueServiceProxy, CommentServiceProxy, ConfigviewServiceProxy } from '@shared/service-proxies/service-proxies';
import {AccordionModule} from 'primeng/accordion';
NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();
import { FileUploadModule } from 'ng2-file-upload';
import { WorkFolowManagementRoutingModule } from './workflow-management-routing.module';
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
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ModalModule,
        WorkFolowManagementRoutingModule,
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
        EditorModule
    ],
    declarations: [
        WorkflowManagementComponent,
        CreateIssueComponent,
        ManagementIssueComponent,
        CommentsComponent,
        CreateOrEditCommentComponent,
        ConfigViewComponent,
        EstimateComponent,
    ],
    providers: [
        ProjectServiceProxy,
        MemberServiceProxy,
        DocumentServiceProxy,
        IssueServiceProxy,
        CommentServiceProxy,
        CommonAppserviceServiceProxy,
        ConfigviewServiceProxy,
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class WorkFlowManagementModule { }
