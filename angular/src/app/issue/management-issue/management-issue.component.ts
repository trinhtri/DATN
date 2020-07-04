import { Component, OnInit, Injector, EventEmitter, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
import { CreateIssueDto, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy, UserServiceProxy, IssueListDto, CommonListDto } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateIssueComponent } from '../create-issue/create-issue.component';
import { EstimateComponent } from './estimate/estimate.component';
import { CreateOrUpdateSprintComponent } from '../create-or-update-sprint/create-or-update-sprint.component';
@Component({
  selector: 'app-management-issue',
  templateUrl: './management-issue.component.html',
  styleUrls: ['./management-issue.component.css'],
  animations: [appModuleAnimation()]

})
export class ManagementIssueComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateIssueComponent;
  @ViewChild('createOrEditSprintModal', { static: true }) createOrEditSprintModal: CreateOrUpdateSprintComponent;

  @ViewChild('estimateComponent', { static: true }) addEstimateTimeModal: EstimateComponent;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: CommonListDto = new CommonListDto();
  idIssue: number;
  assignee: any;
  reporter: any;
  isSprint = false;

  // date
  dueDate: any;
  createdDate = new Date();
  updateDate: any;
  loading = false;
  constructor(injector: Injector,
    private _issueService: IssueServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _userService: UserServiceProxy,
    private _router: Router
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
      if (this.issue.type === 1) {
        this.isSprint = true;
      } else {
        this.isSprint = false;
      }
      console.log('isSprint', this.isSprint);
      this.createdDate = this.issue.creationTime.toDate();
    });
  }

  editIssue(id) {
    this.createOrEditModal.show(id);
  }
  editSprint(id) {
    this.createOrEditSprintModal.show(id);
  }
  startProgress(id) {
    this._issueService.startProgress(id).subscribe(result => {
      this.notify.success(this.l('start'));
      this.ngOnInit();
    });
  }
  stopProgress(id) {
    this._issueService.stopProgress(id).subscribe(result => {
      this.notify.success(this.l('Stop'));
      this.ngOnInit();
    });
  }
  resolved(id) {
    this.addEstimateTimeModal.show(id);
    this._issueService.resolved(id).subscribe(result => {
      this.notify.success(this.l('resolved'));
      this.ngOnInit();
    });
  }
  close(id) {
    this._issueService.closeProgress(id).subscribe(result => {
      this.notify.success(this.l('Close'));
      this.ngOnInit();
    });
  }
  reOpen(id) {
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
    let text = '';
    if (this.isSprint === true) {
      text = 'SprintDeleteWarningMessage';
    } else {
      text = 'IssueDeleteWarningMessage';
    }
    this.message.confirm(
      this.l(text, dto.issueCode),
      this.l('AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this._issueService.delete(dto.id)
            .subscribe(() => {
              this.notify.success(this.l('SuccessfullyDeleted'));
            });
        }
      }
    );
  }
}

