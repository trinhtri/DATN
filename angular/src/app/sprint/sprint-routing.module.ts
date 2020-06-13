import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SprintComponent } from './sprint/sprint.component';
import { ProjectServiceProxy, CommonAppserviceServiceProxy, SprintServiceProxy, IssueServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'sprint', component: SprintComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ],
    providers: [
        ProjectServiceProxy,
        FileDownloadService,
        CommonAppserviceServiceProxy,
        SprintServiceProxy,
        IssueServiceProxy
    ]
})
export class SprintRoutingModule { }
