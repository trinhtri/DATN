import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CreateIssueDto, ERPComboboxItem, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment'
@Component({
  selector: 'app-create-issue-of-sprint',
  templateUrl: './create-issue-of-sprint.component.html',
  styleUrls: ['./create-issue-of-sprint.component.css']
})
export class CreateIssueOfSprintComponent extends AppComponentBase implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: CreateIssueDto = new CreateIssueDto();
  dueDate: any;
  startDate: any;
  active = false;
  saving = false;
  projectId: number;
  lst: ERPComboboxItem[] = [];
  lstSprint: ERPComboboxItem[] = [];
  lstProject: ERPComboboxItem[] = [];
  project_Id: number;
  lstPriority = [
    { id: 1, displayText: this.l('1') },
    { id: 2, displayText: this.l('2') },
    { id: 3, displayText: this.l('3') },
  ];
  lstType = [
    { id: 1, displayText: this.l('NewFeature') },
    { id: 2, displayText: this.l('Improvement') },
    { id: 3, displayText: this.l('Bug') },
  ];
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
    this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe(result => {
      this.lst = result;
    });
    this._commonService.getLookups('Project', this.appSession.tenantId, undefined).subscribe(result => {
      this.lstProject = result;
    });
    this._commonService.getLookups('Sprints', this.appSession.tenantId, undefined).subscribe(result => {
      this.lstSprint = result;
    });
  }
  show(sprintId, id?: number): void {
    this.active = true;
    this.modal.show();
    this.initForm();
    
    if (id) {
      this._issueService.getId(id).subscribe(result => {
        this.issue = result;
        console.log(this.issue);
        if (this.issue.due_Date) {
          this.dueDate = this.issue.due_Date.toDate();
        }
        if (this.issue.startDate) {
          this.startDate = this.issue.startDate.toDate();
        }
      });
    } else {
      // mặc định khi tạo mới thì mức độ là bình thường
      this.issue.sprint_Id = sprintId
      this.issue.priority_ID = 1;
      this.issue.status_Id = 1;
    }
  }
  onShown(): void {
    document.getElementById('IssueCode').focus();
  }
  save(): void {
    this.saving = true;
    this.issue.reporter_Id = this.appSession.userId;
    if (this.dueDate) {
      this.issue.due_Date = moment(this.dueDate);
    }
    if (this.startDate) {
      this.issue.startDate = moment(this.startDate);
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
    this.projectId = null;
    this.dueDate = null;
    this.startDate = null;
    this.lst = [];
    this.lstSprint = [];
    this.issue = new CreateIssueDto();
    this.saving = false;
    this.active = false;
    this.modal.hide();
  }
  changeProject(projectId) {
    this._commonService.getLookups('Sprints', this.appSession.tenantId, projectId).subscribe(result => {
      this.lstSprint = result;
    });
    this._commonService.getLookups('MemberOfProject', this.appSession.tenantId, projectId).subscribe(result => {
      this.lst = result;
    });
  }

}
