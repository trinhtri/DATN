import { Component, OnInit, ViewChild, Output, EventEmitter, Injector, ChangeDetectionStrategy } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CreateMemberDto, MemberServiceProxy, ERPComboboxItem, CommonLookupServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import * as moment from 'moment';
@Component({
  selector: 'app-create-or-edit-member',
  templateUrl: './create-or-edit-member.component.html',
  styleUrls: ['./create-or-edit-member.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateOrEditMemberComponent extends AppComponentBase implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  member: CreateMemberDto = new CreateMemberDto();
  startDate: any;
  endDate: any;
  active = false;
  saving = false;
  lst: ERPComboboxItem[] = [];
  lstRole = [{ value: 1, display: this.l('Manager') },
  { value: 2, display: this.l('Dev') },
  { value: 3, display: this.l('Test') },
  ];
  projectId: any;
  constructor(injector: Injector,
    private _memberService: MemberServiceProxy,
    private _commonService: CommonAppserviceServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
  }
  initForm() {
    this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe(result => {
      this.lst = result;
    });
  }
  show(projectId, id?: number): void {
    this.projectId = projectId;
    console.log('projectId', this.projectId);
    this.initForm();
    this.active = true;
    this.modal.show();
    if (id) {
      this._memberService.getId(id).subscribe(result => {
        this.member = result;
        if (this.member.startDate) {
          this.startDate = this.member.startDate.toDate();
        }
        if (this.member.endDate) {
          this.endDate = this.member.endDate.toDate();
        }
      });
    }
  }
  onShown(): void {
    // document.getElementById('EmployeeName').focus();
  }
  save(): void {
    this.saving = true;
    this.member.project_Id = this.projectId;
    this.member.startDate = moment(this.startDate);
    if (this.endDate) {
      this.member.endDate = moment(this.endDate);
    }
    if (this.member.id) {
      this._memberService.update(this.member)
        .pipe(finalize(() => { this.saving = false; }))
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close();
          this.modalSave.emit(null);
        });
    } else {
      this._memberService.create(this.member)
        .pipe(finalize(() => { this.saving = false; }))
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close();
          this.modalSave.emit(null);
        });
    }
  }

  close(): void {
    this.startDate = null;
    this.endDate = null;
    this.member = new CreateMemberDto();
    this.saving = false;
    this.active = false;
    this.modal.hide();
  }

}
