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
import { PaginatorModule } from 'primeng/primeng';
import { MemberComponent } from './member/member.component';
import { DocumentComponent } from './document/document.component';
import { ProjectServiceProxy } from '@shared/service-proxies/service-proxies';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

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
    ],
    declarations: [
        ProjectComponent,
        CreateOrEditProjectComponent,
        MemberComponent,
        DocumentComponent
    ],
    providers: [
        ProjectServiceProxy,
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class ProjectModule { }
