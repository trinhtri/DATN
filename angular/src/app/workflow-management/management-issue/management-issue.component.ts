import { Component, OnInit, Injector, EventEmitter, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
import { CreateIssueDto, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy, UserServiceProxy, IssueListDto } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';
import { CreateIssueComponent } from '../create-issue/create-issue.component';
@Component({
  selector: 'app-management-issue',
  templateUrl: './management-issue.component.html',
  styleUrls: ['./management-issue.component.css'],
  animations: [appModuleAnimation()]

})
export class ManagementIssueComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', {static: true}) createOrEditModal: CreateIssueComponent;

  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: IssueListDto;
  idIssue: number;
  assignee: any;
  reporter: any;

  // date
  dueDate: any;
  createdDate = new Date();
  updateDate: any;
  constructor(injector: Injector,
    private _projectService: ProjectServiceProxy,
    private _issueService: IssueServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _userService: UserServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this.idIssue = +this._activatedRoute.snapshot.paramMap.get('id');
    this._issueService.getIssueForDetail(+this._activatedRoute.snapshot.paramMap.get('id')).subscribe(result => {
      this.issue = result;
      console.log('projectId', this.issue.project_Id);
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
        this.createdDate = this.issue.creationTime.toDate();
      this.updateDate = this.issue.update_Date.toDate();
    });
  }

  editIssue(id) {
    this.createOrEditModal.show(id);
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
}

