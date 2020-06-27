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

  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: CreateIssueDto = new CreateIssueDto();
  dueDate: any;
  active = false;
  saving = false;
  projectId: number;
  lst: ERPComboboxItem[] = [];
  lstSprint: ERPComboboxItem[] = [];
  lstProject: ERPComboboxItem[] = [];
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
  show(id?: number): void {
    this.active = true;
    this.modal.show();
    if (id) {
      this._issueService.getId(id).subscribe(result => {
        this.issue = result;
        if (this.issue.due_Date) {
          this.dueDate = this.issue.due_Date.toDate();
        }
      });
    } else {
      // mặc định khi tạo mới thì mức độ là bình thường
      this.issue.priority_ID = 1;
    }
    this.initForm();
  }
  onShown(): void {
    document.getElementById('IssueCode').focus();
  }
  save(): void {
    this.saving = true;
    this.issue.type = 2;
    this.issue.reporter_Id = this.appSession.userId;
    this.issue.update_Date = moment(new Date);
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
    this.projectId = null;
    this.dueDate = null;
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
