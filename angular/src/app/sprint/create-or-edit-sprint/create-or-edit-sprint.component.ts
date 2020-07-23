import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { SprintListDto, ERPComboboxItem, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy, SprintServiceProxy, CreateSprintDto, CreateIssueDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import * as moment from 'moment';
@Component({
  selector: 'app-create-or-edit-sprint',
  templateUrl: './create-or-edit-sprint.component.html',
  styleUrls: ['./create-or-edit-sprint.component.css']
})
export class CreateOrEditSprintComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  sprint: CreateSprintDto = new CreateSprintDto();
  dueDate: any;
  mindate = new Date();
  startDate: any;
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
  show(id?: number): void {
    this.active = true;
    this.modal.show();
    if (id) {
      this._sprintService.getId(id).subscribe(result => {
        this.sprint = result;
        console.log('sprint', this.sprint)
        if (this.sprint.startDate) {
          this.startDate = this.sprint.startDate;
          this.mindate = this.startDate.toDate();
        }
        if (this.sprint.due_Date) {
          this.dueDate = this.sprint.due_Date;
        }
        this._commonService.getLookups('MemberOfProject', this.appSession.tenantId, this.sprint.project_Id).subscribe(result => {
            this.lst = result;
            console.log('lst', this.lst);
          });
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
      this.sprint.due_Date = this.dueDate;
    }
    if (this.startDate) {
      this.sprint.startDate = this.startDate;
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
  onChangeProject(projectId){
  this._commonService.getLookups('MemberOfProject', this.appSession.tenantId, projectId).subscribe(result => {
    this.lst = result;
  });
  }
  dateChanged(date){
    this.mindate = date.toDate();
}
}
