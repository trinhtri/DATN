import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { TenantDashboardServiceProxy, SalesSummaryDatePeriod, CreateSprintDto, SprintServiceProxy, CommonAppserviceServiceProxy, ERPComboboxItem } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
@Component({
  selector: 'app-manager-sprint',
  templateUrl: './manager-sprint.component.html',
  styleUrls: ['./manager-sprint.component.css'],
  animations: [appModuleAnimation()]

})



export class ManagerSprintComponent extends AppComponentBase implements OnInit {
  data: any;
  lstDataType: any;
  sprint: CreateSprintDto = new CreateSprintDto();
  dueDate: any;
  startDate: any;
  saving =  false;
  lst: ERPComboboxItem[] = [];
  lstSprint: ERPComboboxItem[] = [];
  lstProject: ERPComboboxItem[] = [];
  sprintId: number;
  constructor(injecter: Injector,
    private _activedRouter: ActivatedRoute,
    private _sprintService: SprintServiceProxy,
    private _commonService: CommonAppserviceServiceProxy,
    private _dashboardService: TenantDashboardServiceProxy) {
    super(injecter);
  }

  ngOnInit() {
    this.sprintId = this._activedRouter.snapshot.params['id'];
    console.log('id', this.sprintId);
    this.initData();
    this._sprintService.getId(this.sprintId).subscribe(result => {
      this.sprint = result;
      if (this.sprint.startDate) {
        this.startDate = this.sprint.startDate.toDate();
      }
      if (this.sprint.due_Date) {
        this.dueDate = this.sprint.due_Date.toDate();
      }
    });
    this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe(result => {
      this.lst = result;
    });
    this._commonService.getLookups('Project', this.appSession.tenantId, undefined).subscribe(result => {
      this.lstProject = result;
    });
  }

  initData() {
    this.data = {
      labels: [this.l('Open'), this.l('InProgress'), this.l('Resolved'), this.l('Compeleted'), this.l('ReOpened')],
      datasets: [
        {
          data: [150, 50],
          backgroundColor: [
            '#FF6384',
            '#36A2EB',
            '#FFCE56'
          ],
          hoverBackgroundColor: [
            '#FF6384',
            '#36A2EB',
            '#FFCE56'
          ]
        }]
    };

    this.lstDataType = {
      labels: [this.l('NewFeature'), this.l('Improvement'), this.l('Bug')],
      datasets: [
        {
          data: [150, 50],
          backgroundColor: [
            '#FF6384',
            '#36A2EB',
            '#FFCE56'
          ],
          hoverBackgroundColor: [
            '#FF6384',
            '#36A2EB',
            '#FFCE56'
          ]
        }]
    };
  }

  save(): void {
    this.saving = true;
    this.sprint.reporter_Id = this.appSession.userId;
    if (this.dueDate) {
      this.sprint.due_Date = moment(this.dueDate);
    }
    if (this.startDate) {
      this.sprint.startDate = moment(this.startDate);
    }
    if (this.sprint.id) {
      this._sprintService.update(this.sprint)
        .pipe(finalize(() => { this.saving = false; }))
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close();
        });
    }
  }
  close() {

  }
}
