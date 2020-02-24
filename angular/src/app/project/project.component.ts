import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrEditUserModalComponent } from '@app/admin/users/create-or-edit-user-modal.component';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { UserServiceProxy, UserListDto } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
  animations: [appModuleAnimation()]

})
export class ProjectComponent extends AppComponentBase {
    @ViewChild('createOrEditUserModal', {static: true}) createOrEditUserModal: CreateOrEditUserModalComponent;
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
        private _userServiceProxy: UserServiceProxy,
        private _fileDownloadService: FileDownloadService,
    ) {
        super(injector);
    }

    getUsers(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._userServiceProxy.getUsers(
            this.filterText,
            this.permission ? this.selectedPermission : undefined,
            this.role !== '' ? parseInt(this.role) : undefined,
            this.onlyLockedUsers,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }
  deleteUser(user: UserListDto): void {
    this.message.confirm(
        this.l('UserDeleteWarningMessage', user.userName),
        this.l('AreYouSure'),
        (isConfirmed) => {
            if (isConfirmed) {
                this._userServiceProxy.deleteUser(user.id)
                    .subscribe(() => {
                        this.reloadPage();
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        }
    );
}
reloadPage(): void {
  this.paginator.changePage(this.paginator.getPage());
}
}
