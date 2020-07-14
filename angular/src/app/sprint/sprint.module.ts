import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SprintRoutingModule } from './sprint-routing.module';
import { FormsModule } from '@angular/forms';
import { ModalModule, TabsModule, TooltipModule, BsDatepickerModule, BsDropdownModule, PopoverModule, AccordionModule } from 'ngx-bootstrap';
import { TableModule } from 'primeng/table';
import { PaginatorModule, FileUploadModule, InputSwitchModule, MultiSelectModule } from 'primeng/primeng';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { SprintComponent } from './sprint/sprint.component';
import { CreateOrEditSprintComponent } from './create-or-edit-sprint/create-or-edit-sprint.component';
import {EditorModule} from 'primeng/editor';
import { ManagerSprintComponent } from './manager-sprint/manager-sprint.component';
import {ChartModule} from 'primeng/chart';
import { InfoCommonSprintComponent } from './manager-sprint/info-common-sprint/info-common-sprint.component';
import { IssuesOfSprintComponent } from './manager-sprint/issues-of-sprint/issues-of-sprint.component';
import { DashBoardSprintComponent } from './manager-sprint/dash-board-sprint/dash-board-sprint.component';
import { CreateIssueOfSprintComponent } from './manager-sprint/issues-of-sprint/create-issue-of-sprint/create-issue-of-sprint.component';
@NgModule({
  declarations: [
     SprintComponent,
     CreateOrEditSprintComponent,
     ManagerSprintComponent,
     InfoCommonSprintComponent,
     IssuesOfSprintComponent,
     DashBoardSprintComponent,
     CreateIssueOfSprintComponent
    ],
  imports: [
    CommonModule,
    FormsModule,
    ModalModule,
    SprintRoutingModule,
    TabsModule,
    TableModule,
    PaginatorModule,
    TooltipModule,
    AppCommonModule,
    UtilsModule,
    NgxChartsModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    PopoverModule.forRoot(),
    AccordionModule,
    FileUploadModule,
    InputSwitchModule,
    MultiSelectModule,
    EditorModule,
    ChartModule
  ],
  exports: [
    CreateOrEditSprintComponent
  ]
})
export class SprintModule { }
