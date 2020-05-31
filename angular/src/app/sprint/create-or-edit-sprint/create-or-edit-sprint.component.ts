import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { SprintListDto, ERPComboboxItem, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy, SprintServiceProxy, CreateSprintDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
  selector: 'app-create-or-edit-sprint',
  templateUrl: './create-or-edit-sprint.component.html',
  styleUrls: ['./create-or-edit-sprint.component.css']
})
export class CreateOrEditSprintComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  sprint: CreateSprintDto = new CreateSprintDto();
  dueDate = new Date();
  active = false;
  saving = false;
  lst: ERPComboboxItem [] = [];
  lstRole: ERPComboboxItem [] = [];
  lstProject: ERPComboboxItem [] = [];
  lstType: ERPComboboxItem [] = [];
  constructor(injector: Injector,
    private _commonService: CommonAppserviceServiceProxy,
    private _sprintService: SprintServiceProxy
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
    console.log('lstProject', this.lstProject);
  });
  this._commonService.getLookups('IssueTypes', this.appSession.tenantId, undefined).subscribe( result => {
    this.lstType = result;
  });
 }
  show(id?: number): void {
    this.active = true;
    this.modal.show();
    if (id) {
    this._sprintService.getId(id).subscribe( result => {
      this.sprint = result;
    });
  }
}
onShown(): void {
  document.getElementById('Project').focus();
}
save(): void {
  this.saving = true;
  if (this.sprint.id) {
    this._sprintService.update(this.sprint)
    .pipe(finalize(() => { this.saving = false; }))
    .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
    });
  } else {
    // mặc định khi tạo mới thì resolve là 1: chưa hoàn thành
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
  this.dueDate = null;
  this.sprint = new CreateSprintDto();
  this.saving = false;
  this.active = false;
  this.modal.hide();
}
}
