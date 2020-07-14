import { Component, OnInit, Injector, OnDestroy, ViewRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ProjectServiceProxy, ProjectListDto, CreateProjectDto, ThemeSettingsDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-manager-project',
  templateUrl: './manager-project.component.html',
  styleUrls: ['./manager-project.component.less'],
  animations: [appModuleAnimation()]
})
export class ManagerProjectComponent  extends AppComponentBase implements OnInit  {
  id: any;
  project: CreateProjectDto = new CreateProjectDto();
  startDate: any;
  endDate: any;
  status: any;
  assignee: string;
  reporter: string;
  constructor(injector: Injector,
    private _activatedRoute: ActivatedRoute,
    private _projectService: ProjectServiceProxy,
    private _userService: UserServiceProxy,
    ) {
      super(injector);
     }

  ngOnInit() {
    this.id = this._activatedRoute.snapshot.paramMap.get('id');
    console.log('id', this.id);
    this._projectService.getId(this.id).subscribe( result => {
      this.project = result;
      this.startDate = this.project.startDate.toDate();
      if (this.project.endDate) {
        this.endDate = this.project.endDate.toDate();
      }
      if (this.project.status === true) {
        this.status = this.l('Đang hoạt động');
      } else {
        this.status = this.l('Ngừng hoạt động');
      }
      this._userService.getName(this.project.reporter_Id).subscribe(result => {
        this.reporter = result;
      });
      this._userService.getName(this.project.assignee_Id).subscribe(result => {
        this.assignee = result;
      });
    });
  }
}
