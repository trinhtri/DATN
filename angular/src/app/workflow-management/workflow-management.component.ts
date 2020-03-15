import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { LazyLoadEvent } from 'primeng/api';
import { finalize } from 'rxjs/operators';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/primeng';
import { ProjectServiceProxy, ProjectListDto, IssueServiceProxy, IssueListDto, CommonAppserviceServiceProxy, ERPComboboxItem } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { CreateIssueComponent } from './create-issue/create-issue.component';

@Component({
  selector: 'app-workflow-management',
  templateUrl: './workflow-management.component.html',
  styleUrls: ['./workflow-management.component.css'],
  animations: [appModuleAnimation()]
})
export class WorkflowManagementComponent extends AppComponentBase implements OnInit {

  @ViewChild('createOrEditModal', {static: true}) createOrEditModal: CreateIssueComponent;
  @ViewChild('dataTable', {static: true}) dataTable: Table;
  @ViewChild('paginator', {static: true}) paginator: Paginator;
  lstProject: ERPComboboxItem [] = [];
  lstMember: ERPComboboxItem [] = [];
  //Filters
//   a: IssueListDto
  filterText = '';
  projectId: any;
  constructor(
      injector: Injector,
      private _projectServiceProxy: ProjectServiceProxy,
      private _issueServiceProxy: IssueServiceProxy,
      private _fileDownloadService: FileDownloadService,
      private _commonService: CommonAppserviceServiceProxy

  ) {
      super(injector);
  }
    ngOnInit(): void {
        this.initForm();
    }
  initForm() {
    this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe( result => {
     this.lstMember = result;
   });
   this._commonService.getLookups('Project', this.appSession.tenantId, undefined).subscribe( result => {
    this.lstProject = result;
  });
 }
  getAll(event?: LazyLoadEvent) {
      if (this.primengTableHelper.shouldResetPaging(event)) {
          this.paginator.changePage(0);

          return;
      }

      this.primengTableHelper.showLoadingIndicator();

      this._issueServiceProxy.getAll(
          this.projectId,
          this.filterText,
          this.primengTableHelper.getSorting(this.dataTable),
          this.primengTableHelper.getMaxResultCount(this.paginator, event),
          this.primengTableHelper.getSkipCount(this.paginator, event)
      ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
          this.primengTableHelper.totalRecordsCount = result.totalCount;
          this.primengTableHelper.records = result.items;
          this.primengTableHelper.hideLoadingIndicator();
      });
  }
  createNew() {
      this.createOrEditModal.show();
  }
  editProject(id) {
      this.createOrEditModal.show(id);
  }
exportExcel(event?: LazyLoadEvent) {
  this._projectServiceProxy.getProjectToExcel(
      this.filterText,
      this.primengTableHelper.getSorting(this.dataTable),
      this.primengTableHelper.getMaxResultCount(this.paginator, event),
      this.primengTableHelper.getSkipCount(this.paginator, event)
  ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
  this._fileDownloadService.downloadTempFile(result);
  });
}
reloadPage(): void {
this.paginator.changePage(this.paginator.getPage());
}
getType(input: number) {
    if (input === 1) {
        return this.l('NewFeature');
    } else if (input === 2) {
    return this.l('Improvement');
    } else {
        return this.l('Bug');
    }
}
}
