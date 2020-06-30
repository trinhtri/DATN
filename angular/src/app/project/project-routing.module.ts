import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProjectComponent } from './project.component';
import { ManagerProjectComponent } from './manager-project/manager-project.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'projects', component: ProjectComponent, data: { permission: 'Pages.Project' } },
                    { path: 'manager-project/:id', component: ManagerProjectComponent, data: { permission: 'Pages.Project.Manager' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class ProjectRoutingModule { }
