import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SprintComponent } from './sprint/sprint.component';
import { ProjectServiceProxy, CommonAppserviceServiceProxy, SprintServiceProxy, IssueServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { ManagerSprintComponent } from './manager-sprint/manager-sprint.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'sprint', component: SprintComponent, data: { permission: 'Pages.Sprint' } },
                    { path: 'sprint/:id', component: ManagerSprintComponent, data: { permission: 'Pages.Sprint' } },
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
