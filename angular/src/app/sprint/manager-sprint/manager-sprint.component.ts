import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { TenantDashboardServiceProxy, SalesSummaryDatePeriod, CreateSprintDto, SprintServiceProxy, CommonAppserviceServiceProxy, ERPComboboxItem, IssueListOfSprintDto, IssueServiceProxy, ScaleStatusIssueOfSprintForChart, ScaleTypeIssueOfSprintForChart } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
@Component({
  selector: 'app-manager-sprint',
  templateUrl: './manager-sprint.component.html',
  styleUrls: ['./manager-sprint.component.css'],
  animations: [appModuleAnimation()]

})

export class ManagerSprintComponent extends AppComponentBase implements OnInit {

  lstType = [
    { value: 1, displayText: this.l('NewFeature') },
    { value: 2, displayText: this.l('Improvement') },
    { value: 3, displayText: this.l('Bug') },
  ];
  lstStatus = [
    { value: 1, displayText: this.l('Open') },
    { value: 2, displayText: this.l('InProgress') },
    { value: 3, displayText: this.l('Resolved') },
    { value: 4, displayText: this.l('Compeleted') },
    { value: 5, displayText: this.l('ReOpened') },
  ];

  data: any;
  lstDataType: any;
  lstStatusSelected: any;
  lstTypeSelected: any;
  sprint: CreateSprintDto = new CreateSprintDto();
  dueDate: any;
  startDate: any;
  saving = false;
  lst: ERPComboboxItem[] = [];
  lstSprint: ERPComboboxItem[] = [];
  lstProject: ERPComboboxItem[] = [];
  lstIssueForStatus: IssueListOfSprintDto[] = [];
  lstIssueForType: IssueListOfSprintDto[] = [];
  sprintId: number;
  scaleStatus: ScaleStatusIssueOfSprintForChart = new ScaleStatusIssueOfSprintForChart();
  scaleType: ScaleTypeIssueOfSprintForChart = new ScaleTypeIssueOfSprintForChart();

  constructor(injecter: Injector,
    private _activedRouter: ActivatedRoute,
    private _sprintService: SprintServiceProxy,
    private _issueService: IssueServiceProxy,
    private _commonService: CommonAppserviceServiceProxy,
    private _router: Router,
    private _dashboardService: TenantDashboardServiceProxy) {
    super(injecter);
  }

  ngOnInit() {
    this.sprintId = this._activedRouter.snapshot.params['id'];
    console.log('id', this.sprintId);
    this.initDataForChart();
    this.initDataForTable();
  }
  initDataForTable() {
    this._sprintService.getId(this.sprintId).subscribe(result => {
      this.sprint = result;
      console.log('sprint', this.sprint);
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
    this.getIssueForStatus();
    this.getIssueForType();
  }

  getIssueForStatus() {
    this._issueService.getIssuesStatusOfSprint(this.sprintId , this.lstStatusSelected).subscribe(result => {
      this.lstIssueForStatus = result;
      console.log('lstIssueForStatus', this.lstIssueForStatus);
    });
  }

  getIssueForType() {
    this._issueService.getIssuesTypeOfSprint(this.sprintId, this.lstTypeSelected).subscribe(result => {
      this.lstIssueForType = result;
    });
  }



  initDataForChart() {
    this._issueService.getScaleStatusForChart(this.sprintId).subscribe(result => {
      this.scaleStatus = result;
      this.data = {
        labels: [this.l('Open'), this.l('InProgress'), this.l('Resolved'), this.l('Compeleted'), this.l('ReOpened')],
        datasets: [
          {
            data: [this.scaleStatus.scaleOpen, this.scaleStatus.scaleInProgress, this.scaleStatus.scaleResolved, this.scaleStatus.scaleCompeleted, this.scaleStatus.scaleReOpened],
            backgroundColor: [
              '#5d78ff',
              '#0abb87',
              '#FFCE56',
              '#343a40',
              '#bc4f4f '
            ],
            hoverBackgroundColor: [
              '#5d78ff',
              '#0abb87',
              '#FFCE56',
              '#343a40',
              '#d71212'
            ]
          }]
      };
    });

    this._issueService.getScaleTypeForChart(this.sprintId).subscribe(result => {
      this.scaleType = result;
      console.log('scaleType', this.scaleType);
      this.lstDataType = {
        labels: [this.l('NewFeature'), this.l('Improvement'), this.l('Bug')],
        datasets: [
          {
            data: [this.scaleType.scaleNewFeature, this.scaleType.scaleImprovent, this.scaleType.scaleBug],
            backgroundColor: [
              '#0abb87',
              '#5867dd',
              '#bc4f4f'
            ],
            hoverBackgroundColor: [
              '#0abb87',
              '#5867dd',
              '#d71212'
            ]
          }]
      };
    });
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
    this._router.navigate(['app/sprint/sprint']);
  }

  getTypeName(id) {
    switch (id) {
      case 1:
        return this.l('NewFeature');
        break;
      case 2:
        return this.l('Improvement');
        break;
      case 3:
        return this.l('Bug');
        break;
    }
  }

  getStatusName(id) {
    switch (id) {
      case 1:
        return this.l('Open');
        break;
      case 2:
        return this.l('InProgress');
        break;
      case 3:
        return this.l('Resolved');
        break;
      case 4:
        return this.l('Compeleted');
        break;
      case 5:
        return this.l('ReOpened');
        break;
    }
  }
  changeStatus(value) {
  console.log('changeStatus', value);
  this.getIssueForStatus();

  }
  changeType(value) {
    console.log('changeType', value);
    console.log('lst type', this.lstTypeSelected);
    this.getIssueForType();
  }
  onClickIssue(id) {
    this._router.navigate(['/app/issue/management-issue', id]);
}
}
