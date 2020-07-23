import { Component, OnInit, Injector, EventEmitter, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
import { CreateIssueDto, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy, UserServiceProxy, IssueListDto, CommonListDto, CreateHistoryStatusIssueDto, HistoryStatusIssueServiceProxy } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateIssueComponent } from '../create-issue/create-issue.component';
import { EstimateComponent } from './estimate/estimate.component';
import { CreateOrUpdateSprintComponent } from '../create-or-update-sprint/create-or-update-sprint.component';
import { HistoryStatusIssueComponent } from '../history-status-issue/history-status-issue.component';
@Component({
  selector: 'app-management-issue',
  templateUrl: './management-issue.component.html',
  styleUrls: ['./management-issue.component.less'],
  animations: [appModuleAnimation()]

})
export class ManagementIssueComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateIssueComponent;
  @ViewChild('createOrEditSprintModal', { static: true }) createOrEditSprintModal: CreateOrUpdateSprintComponent;
  @ViewChild('historyStatusIssueComponent', {static: true}) historyStatusIssueComponent: HistoryStatusIssueComponent;
  @ViewChild('estimateComponent', { static: true }) addEstimateTimeModal: EstimateComponent;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: CommonListDto = new CommonListDto();
  idIssue: number;
  assignee: any;
  reporter: any;
  isSprint = false;
  statusName
  // date
  dueDate: any;
  createdDate = new Date();
  updateDate: any;
  startDate: any;

  loading = false;
  constructor(injector: Injector,
    private _issueService: IssueServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _userService: UserServiceProxy,
    private _router: Router,
    private _historyStatusIssueService: HistoryStatusIssueServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this.loading = true;
    this.idIssue = +this._activatedRoute.snapshot.paramMap.get('id');
    this._issueService.getIssueForDetail(this.idIssue).pipe(finalize(() => this.loading = false)).subscribe(result => {
      this.issue = result;
      console.log('issue', this.issue);
      // lấy tên của assignee và Reporter
      this._userService.getName(this.issue.reporter_Id).subscribe(result => {
        this.reporter = result;
      });
      this._userService.getName(this.issue.assignee_Id).subscribe(result => {
        this.assignee = result;
      });

      // date
      if (this.issue.due_Date) {
        this.dueDate = this.issue.due_Date;
      }
      if(this.issue.startDate){
          this.startDate = this.issue.startDate.toDate();
      }
      this.createdDate = this.issue.creationTime.toDate();
      this.statusName= this.getStatusName(this.issue.status_Id)
      console.log('statusName', this.statusName)
    });
  }

  editIssue(id) {
    this.createOrEditModal.show(id);
  }
  editSprint(id) {
    this.createOrEditSprintModal.show(id);
  }
  startProgress(id) {
    let history = new CreateHistoryStatusIssueDto();
    history.issue_Id = this.issue.id;
    history.user_Id = this.appSession.userId;
    history.oldValue = this.statusName;
    history.newValue = 'Đang xử lý'
    this._historyStatusIssueService.create(history).subscribe(result =>{
    this.historyStatusIssueComponent.getAll()
    })

    this._issueService.startProgress(id).subscribe(result => {
      this.notify.success(this.l('start'));
      this.ngOnInit();
    });
  }
  stopProgress(id) {
    let history = new CreateHistoryStatusIssueDto();
    history.issue_Id = this.issue.id;
    history.user_Id = this.appSession.userId;
    history.oldValue = this.statusName;
    history.newValue = 'Ngừng xử lý'
    this._historyStatusIssueService.create(history).subscribe(result =>{
    this.historyStatusIssueComponent.getAll()
    })
    let oldvalue = this.getStatusName(this.issue.status_Id)
    console.log('oldvalue', oldvalue)
    this._issueService.stopProgress(id).subscribe(result => {
      this.notify.success(this.l('Stop'));
      this.ngOnInit();
    });
  }
  estimate(id) {
    this.addEstimateTimeModal.show(id);
  }
  resolved(id) {
    let history = new CreateHistoryStatusIssueDto();
    history.issue_Id = this.issue.id;
    history.user_Id = this.appSession.userId;
    history.oldValue = this.statusName;
    history.newValue = 'Đã giải quyết'
    this._historyStatusIssueService.create(history).subscribe(result =>{
    this.historyStatusIssueComponent.getAll()
    })

    let oldvalue = this.getStatusName(this.issue.status_Id)
    console.log('oldvalue', oldvalue)
    this._issueService.resolved(id).subscribe(result => {
      this.notify.success(this.l('resolved'));
      this.ngOnInit();
    });
  }
  close(id) {
    let history = new CreateHistoryStatusIssueDto();
    history.issue_Id = this.issue.id;
    history.user_Id = this.appSession.userId;
    history.oldValue = this.statusName;
    history.newValue = 'Đã hoàn thành'
    this._historyStatusIssueService.create(history).subscribe(result =>{
    this.historyStatusIssueComponent.getAll()
    })
    let oldvalue = this.getStatusName(this.issue.status_Id)
    console.log('oldvalue', oldvalue)
    this._issueService.closeProgress(id).subscribe(result => {
      this.notify.success(this.l('Close'));
      this.ngOnInit();
    });
  }
  reOpen(id) {
    let history = new CreateHistoryStatusIssueDto();
    history.issue_Id = this.issue.id;
    history.user_Id = this.appSession.userId;
    history.oldValue = this.statusName;
    history.newValue = 'Mở lại'
    this._historyStatusIssueService.create(history).subscribe(result =>{
    this.historyStatusIssueComponent.getAll()
    })

    let oldvalue = this.getStatusName(this.issue.status_Id)
    console.log('oldvalue', oldvalue)
    this._issueService.reOpenProgress(id).subscribe(result => {
      this.notify.success(this.l('reOpen'));
      this.ngOnInit();
    });
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
  clickIssue(id) {
    let route = '/app/issue/management-issue/' + id;
    window.open(route);
  }
  delete(dto): void {
    console.log(dto);
    this.message.confirm(
      this.l('IssueDeleteWarningMessage', dto.taskCode),
      this.l('AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this._issueService.delete(dto.id)
            .subscribe(() => {
              this.notify.success(this.l('SuccessfullyDeleted'));
              this._router.navigate(['/app/issue/management'])
            });
        }
      }
    );
  }
}

