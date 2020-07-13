import { Component, OnInit, Input, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { SprintServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';
@Component({
  selector: 'app-info-common-sprint',
  templateUrl: './info-common-sprint.component.html',
  styleUrls: ['./info-common-sprint.component.css']
})
export class InfoCommonSprintComponent extends AppComponentBase implements OnInit {
  @Input() sprintId: number;4
  sprint: any;
  startDate: any;
  dueDate: any;
  lst: any;
  lstProject: any;
  saving = false;
  constructor(injector: Injector,
    private _sprintService: SprintServiceProxy,
    private _issueService: IssueServiceProxy,
    private _commonService: CommonAppserviceServiceProxy,
    private _router: Router
    ) {
    super(injector)
  }
  ngOnInit() {
    this.initDataForTable();
  }
  initDataForTable() {
    this.saving = true;
    this._sprintService.getId(this.sprintId).pipe(finalize(() => { this.saving = false; })).subscribe(result => {
      this.sprint = result;
      console.log('sprint', this.sprint);
      if (this.sprint.startDate) {
        this.startDate = this.sprint.startDate.toDate();
      }
      if (this.sprint.due_Date) {
        this.dueDate = this.sprint.due_Date.toDate();
      }
    });
    this._commonService.getLookups('Member', this.appSession.tenantId, undefined).pipe(finalize(() => { this.saving = false; })).subscribe(result => {
      this.lst = result;
    });
    this._commonService.getLookups('Project', this.appSession.tenantId, undefined).pipe(finalize(() => { this.saving = false; })).subscribe(result => {
      this.lstProject = result;
    });
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
          // this.close();
        });
    }
  }
  close() {
    this.saving = false;
    this._router.navigate(['app/sprint/sprint']);
  }

}
