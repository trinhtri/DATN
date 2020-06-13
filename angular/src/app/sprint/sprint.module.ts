import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SprintRoutingModule } from './sprint-routing.module';
import { FormsModule } from '@angular/forms';
import { ModalModule, TabsModule, TooltipModule, BsDatepickerModule, BsDropdownModule, PopoverModule, AccordionModule } from 'ngx-bootstrap';
import { TableModule } from 'primeng/table';
import { PaginatorModule, FileUploadModule, InputSwitchModule } from 'primeng/primeng';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import CountoModule from 'angular2-counto';
import { SprintComponent } from './sprint/sprint.component';
import { CreateOrEditSprintComponent } from './create-or-edit-sprint/create-or-edit-sprint.component';
import {EditorModule} from 'primeng/editor';

@NgModule({
  declarations: [
     SprintComponent,
     CreateOrEditSprintComponent
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
    CountoModule,
    NgxChartsModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    PopoverModule.forRoot(),
    AccordionModule,
    FileUploadModule,
    InputSwitchModule,
    EditorModule
  ]
})
export class SprintModule { }
