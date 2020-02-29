import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrEditUserModalComponent } from '@app/admin/users/create-or-edit-user-modal.component';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { UserServiceProxy, UserListDto, ProjectServiceProxy, ProjectListDto } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { finalize } from 'rxjs/operators';
import { CreateOrEditProjectComponent } from './create-or-edit-project/create-or-edit-project.component';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
  animations: [appModuleAnimation()]

})
export class ProjectComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', {static: true}) createOrEditModal: CreateOrEditProjectComponent;
    @ViewChild('dataTable', {static: true}) dataTable: Table;
    @ViewChild('paginator', {static: true}) paginator: Paginator;
    //Filters
    advancedFiltersAreShown = false;
    filterText = '';
    selectedPermission = '';
    role = '';
    onlyLockedUsers = false;

    constructor(
        injector: Injector,
        private _projectServiceProxy: ProjectServiceProxy,
        private _fileDownloadService: FileDownloadService,
    ) {
        super(injector);
    }

    getAll(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._projectServiceProxy.getAll(
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
  delete(dto: ProjectListDto): void {
    this.message.confirm(
        this.l('ProjectDeleteWarningMessage', dto.projectName),
        this.l('AreYouSure'),
        (isConfirmed) => {
            if (isConfirmed) {
                this._projectServiceProxy.delete(dto.id)
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
}
