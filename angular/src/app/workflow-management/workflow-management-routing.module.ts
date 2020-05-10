import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { WorkflowManagementComponent } from './workflow-management.component';
import { ManagementIssueComponent } from './management-issue/management-issue.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'management', component: WorkflowManagementComponent, data: { permission: 'Pages.Issue' } },
                    { path: 'management-issue/:id', component: ManagementIssueComponent, data: { permission: 'Pages.Issue' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class WorkFolowManagementRoutingModule { }
