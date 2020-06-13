import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { Table } from 'primeng/table';
import { CreateOrEditProjectComponent } from '@app/project/create-or-edit-project/create-or-edit-project.component';
import { ProjectServiceProxy, ProjectListDto, SprintServiceProxy, SprintListDto, IssueServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSprintComponent } from '../create-or-edit-sprint/create-or-edit-sprint.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-sprint',
  templateUrl: './sprint.component.html',
  styleUrls: ['./sprint.component.css'],
  animations: [appModuleAnimation()]
})
export class SprintComponent extends AppComponentBase implements OnInit {

  @ViewChild('createOrEditModal', {static: true}) createOrEditModal: CreateOrEditSprintComponent;
  @ViewChild('dataTable', {static: true}) dataTable: Table;
  @ViewChild('paginator', {static: true}) paginator: Paginator;
  //Filters
  filterText = '';
  checked =  true;
  constructor(
      injector: Injector,
      private _projectServiceProxy: ProjectServiceProxy,
      private _fileDownloadService: FileDownloadService,
      private _sprintService: SprintServiceProxy,
      private _issueService:  IssueServiceProxy
  ) {
      super(injector);
  }
  ngOnInit(): void {
  }

  getAll(event?: LazyLoadEvent) {
      if (this.checked as any === 'undefined') {
        this.checked = undefined;
      }
      if (this.primengTableHelper.shouldResetPaging(event)) {
          this.paginator.changePage(0);

          return;
      }

      this.primengTableHelper.showLoadingIndicator();

      this._issueService.getAll(
          undefined,
          undefined,
          undefined,
          undefined,
          this.filterText,
          1,
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
  editSprint(id) {
      this.createOrEditModal.show(id);
  }
delete(dto: SprintListDto): void {
  this.message.confirm(
      this.l('SprintDeleteWarningMessage', dto.sprintName),
      this.l('AreYouSure'),
      (isConfirmed) => {
          if (isConfirmed) {
              this._sprintService.delete(dto.id)
                  .subscribe(() => {
                      this.reloadPage();
                      this.notify.success(this.l('SuccessfullyDeleted'));
                  });
          }
      }
  );
}
exportExcel(event?: LazyLoadEvent) {
  this._projectServiceProxy.getProjectToExcel(
      this.filterText,
      this.checked,
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
getTypeName(id) {
    switch (id) {
        case 1:
            return this.l('NewFeature');
            break;
        case 2:
            return this.l('Improvement');
            break;
        case 3:
            return this.l('Bug');
            break;
    }
}
}
