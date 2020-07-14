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
import { ProjectRoutingModule } from './project-routing.module';
import { ProjectComponent } from './project.component';
import { CreateOrEditProjectComponent } from './create-or-edit-project/create-or-edit-project.component';
import { TableModule } from 'primeng/table';
import { PaginatorModule, MultiSelectModule, EditorModule } from 'primeng/primeng';
import { MemberComponent } from './member/member.component';
import { DocumentComponent } from './document/document.component';
import { ProjectServiceProxy, MemberServiceProxy, CommonAppserviceServiceProxy, DocumentServiceProxy } from '@shared/service-proxies/service-proxies';
import {AccordionModule} from 'primeng/accordion';
import { ManagerProjectComponent } from './manager-project/manager-project.component';
import { CreateOrEditMemberComponent } from './member/create-or-edit-member/create-or-edit-member.component';
import { CreateOrEditDocumentComponent } from './document/create-or-edit-document/create-or-edit-document.component';
NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();
import { FileUploadModule } from 'ng2-file-upload';
import {InputSwitchModule} from 'primeng/inputswitch';
import { SprintOfProjectComponent } from './sprint-of-project/sprint-of-project.component';
import { SprintModule } from '@app/sprint/sprint.module';
import { CreateSprintOfProjectComponent } from './sprint-of-project/create-sprint-of-project/create-sprint-of-project.component';
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ModalModule,
        ProjectRoutingModule,
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
        MultiSelectModule,
        SprintModule,
        EditorModule
    ],
    declarations: [
        ProjectComponent,
        CreateOrEditProjectComponent,
        MemberComponent,
        DocumentComponent,
        ManagerProjectComponent,
        CreateOrEditMemberComponent,
        CreateOrEditDocumentComponent,
        SprintOfProjectComponent,
        CreateSprintOfProjectComponent
    ],
    providers: [
        ProjectServiceProxy,
        MemberServiceProxy,
        DocumentServiceProxy,
        CommonAppserviceServiceProxy,
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ],
})
export class ProjectModule { }
