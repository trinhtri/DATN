import { Component, OnInit, ViewChild, EventEmitter, Output, Injector } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { CreateSprintDto, ERPComboboxItem, ProjectServiceProxy, IssueServiceProxy, SprintServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment'
@Component({
  selector: 'app-create-issue-of-project',
  templateUrl: './create-sprint-of-project.component.html',
  styleUrls: ['./create-sprint-of-project.component.css']
})
export class CreateSprintOfProjectComponent extends AppComponentBase implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  sprint: CreateSprintDto = new CreateSprintDto();
  dueDate: any;
  startDate: any;
  active = false;
  saving = false;
  projectId: number;
  mindate = new Date()
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
    private _sprintService: SprintServiceProxy,
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
  show(project_Id: number ,id?: number): void {
    this.active = true;
    this.modal.show();
    this.sprint.project_Id = project_Id
    this._commonService.getLookups('MemberOfProject', this.appSession.tenantId, project_Id).subscribe(result => {
      this.lst = result;
    });

    if (id) {
      this._sprintService.getId(id).subscribe(result => {
        this.sprint = result;
        if (this.sprint.startDate) {
          this.startDate = this.sprint.startDate.toDate();
          this.mindate = this.sprint.startDate.toDate();
        }
        if (this.sprint.due_Date) {
          this.dueDate = this.sprint.due_Date.toDate();
          this.sprint.project_Id = project_Id;
        }
      });
    }
  }
  onShown(): void {
    // document.getElementById('IssueCode').focus();
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
          this.modalSave.emit(null);
        });
    } else {
      // mặc định là mở
      this.sprint.status_Id = 2;
      this._sprintService.create(this.sprint)
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
    this.startDate = null;
    this.dueDate = null;
    this.sprint = new CreateSprintDto();
    this.saving = false;
    this.active = false;
    this.modal.hide();
  }
}
