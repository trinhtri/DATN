import { Component, OnInit, Injector } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ProjectServiceProxy, ProjectListDto, CreateProjectDto, ThemeSettingsDto } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-manager-project',
  templateUrl: './manager-project.component.html',
  styleUrls: ['./manager-project.component.css'],
  animations: [appModuleAnimation()]
})
export class ManagerProjectComponent  extends AppComponentBase implements OnInit {
  id: any;
  project: CreateProjectDto = new CreateProjectDto();
  startDate: any;
  endDate: any;
  status: any;
  constructor(injector: Injector,
    private _activatedRoute: ActivatedRoute,
    private _projectService: ProjectServiceProxy,
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
    });
  }
}
