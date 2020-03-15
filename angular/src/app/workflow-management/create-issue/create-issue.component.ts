import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ProjectServiceProxy, CreateIssueDto, IssueServiceProxy, CommonAppserviceServiceProxy, ERPComboboxItem } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
@Component({
  selector: 'app-create-issue',
  templateUrl: './create-issue.component.html',
  styleUrls: ['./create-issue.component.css']
})
export class CreateIssueComponent extends AppComponentBase implements OnInit {

  @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: CreateIssueDto = new CreateIssueDto();
  dueDate = new Date();
  active = false;
  saving = false;
  lst: ERPComboboxItem [] = [];
  lstRole: ERPComboboxItem [] = [];
  lstProject: ERPComboboxItem [] = [];
  lstType: ERPComboboxItem [] = [];
  lstPriority: ERPComboboxItem [] = [];
  constructor(injector: Injector,
    private _projectService: ProjectServiceProxy,
    private _issueService: IssueServiceProxy,
    private _commonService: CommonAppserviceServiceProxy
    ) {
    super(injector);
   }

  ngOnInit() {
    this.initForm();
  }
  initForm() {
    this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe( result => {
     this.lst = result;
   });
   this._commonService.getLookups('RoleProject', this.appSession.tenantId, undefined).subscribe( result => {
     this.lstRole = result;
   });
   this._commonService.getLookups('Project', this.appSession.tenantId, undefined).subscribe( result => {
    this.lstProject = result;
  });
  this._commonService.getLookups('Priorities', this.appSession.tenantId, undefined).subscribe( result => {
    this.lstPriority = result;
  });
  this._commonService.getLookups('IssueTypes', this.appSession.tenantId, undefined).subscribe( result => {
    this.lstType = result;
  });
 }
  show(id?: number): void {
    this.active = true;
    this.modal.show();
    if (id) {
    this._issueService.getId(id).subscribe( result => {
      this.issue = result;
      if (this.issue.due_Date) {
        this.dueDate = this.issue.due_Date.toDate();
      }
      // if (this.project.endDate) {
      //   this.endDate = this.project.endDate.toDate();
      // }
    });
  } else {
  // mặc định khi tạo mới thì mức độ là bình thường
  this.issue.priority_ID = 1;
  }
}
onShown(): void {
  document.getElementById('ProjectCode').focus();
}
save(): void {
  this.saving = true;
  this.issue.reporter_Id = this.appSession.userId;
//   this.project.startDate = moment(this.startDate);
  if (this.dueDate) {
  this.issue.due_Date = moment(this.dueDate);
  }
  if (this.issue.id) {
    this._issueService.update(this.issue)
    .pipe(finalize(() => { this.saving = false; }))
    .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
    });
  } else {
    // mặc định khi tạo mới thì resolve là 1: chưa hoàn thành
    this.issue.resolve_Id = 1;
    // mặc định là mở
    this.issue.status_Id = 1;
    this._issueService.create(this.issue)
    .pipe(finalize(() => { this.saving = false; }))
    .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
    });
  }
}

close(): void {
  this.dueDate = null;
  this.issue = new CreateIssueDto();
  this.saving = false;
  this.active = false;
  this.modal.hide();
}

}