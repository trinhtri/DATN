import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProjectComponent } from './project.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'projects', component: ProjectComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class ProjectRoutingModule { }
