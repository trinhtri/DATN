import { Component, OnInit, Injector, ViewChild, Output, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateIssueDto, ERPComboboxItem, ProjectServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';
@Component({
  selector: 'app-estimate',
  templateUrl: './estimate.component.html',
  styleUrls: ['./estimate.component.css']
})
export class EstimateComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  issue: CreateIssueDto = new CreateIssueDto();
  active = false;
  saving = false;
  lst: ERPComboboxItem[] = [];
  lstRole: ERPComboboxItem[] = [];
  lstProject: ERPComboboxItem[] = [];
  lstType: ERPComboboxItem[] = [];
  lstPriority: ERPComboboxItem[] = [];
  constructor(injector: Injector,
    private _projectService: ProjectServiceProxy,
    private _issueService: IssueServiceProxy,
    private _commonService: CommonAppserviceServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
  }
  show(id?: number): void {
    this.active = true;
    this.modal.show();
    if (id) {
      this._issueService.getId(id).subscribe(result => {
        this.issue = result;
      });
    } else {
      // mặc định khi tạo mới thì mức độ là bình thường
      this.issue.priority_ID = 1;
    }
  }
  onShown(): void {
    document.getElementById('IssueCode').focus();
  }
  save(): void {
    this.saving = true;
    this.issue.reporter_Id = this.appSession.userId;
    this._issueService.update(this.issue)
      .pipe(finalize(() => { this.saving = false; }))
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
      });
  }

  close(): void {
    this.issue = new CreateIssueDto();
    this.saving = false;
    this.active = false;
    this.modal.hide();
  }

}
