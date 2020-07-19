import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { Table } from 'primeng/table';
import { CreateOrEditProjectComponent } from '@app/project/create-or-edit-project/create-or-edit-project.component';
import { ProjectServiceProxy, ProjectListDto, SprintServiceProxy, SprintListDto, IssueServiceProxy, CommonAppserviceServiceProxy, ERPComboboxItem } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSprintComponent } from '../create-or-edit-sprint/create-or-edit-sprint.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Router } from '@angular/router';

@Component({
    selector: 'app-sprint',
    templateUrl: './sprint.component.html',
    styleUrls: ['./sprint.component.css'],
    animations: [appModuleAnimation()]
})
export class SprintComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateOrEditSprintComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    //Filters
    filterText = '';
    advancedFiltersAreShown: any;
    checked = true;
    lstMember: ERPComboboxItem[] = [];
    lstProject: ERPComboboxItem[] = [];
    lstStatus = [
        { value: 1, displayText: this.l('Active') },
        { value: 2, displayText: this.l('Open') },
        { value: 3, displayText: this.l('Close') },
        { value: 4, displayText: this.l('Cancel') },
    ];
    lstStatusId: number[] = [];
    lstProjectId: number[] = [];
    lstAssignId: number[] = [];

    constructor(
        injector: Injector,
        private _fileDownloadService: FileDownloadService,
        private _sprintService: SprintServiceProxy,
        private _commonService: CommonAppserviceServiceProxy,
        private _router: Router
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.initForm();
    }
    initForm() {
        this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstMember = result;
        });
        this._commonService.getLookups('Project', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstProject = result;
        });
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

        this._sprintService.getAll(
            this.filterText,
            this.lstProjectId,
            this.lstStatusId,
            this.lstAssignId,
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
    delete(dto): void {
        console.log(dto);
        this.message.confirm(
            this.l('SprintDeleteWarningMessage', dto.sprintCode),
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
        this._sprintService.getSprintForExcel(
            this.filterText,
            this.lstProjectId,
            this.lstStatusId,
            this.lstAssignId,
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

    getStatusName(statusId) {
        switch (statusId) {
            case 1:
                return this.l('Active');
                break;
            case 2:
                return this.l('Open');
                break;
            case 3:
                return this.l('Close');
                break;
            case 4:
                return this.l('Cancel');
                break;
        }
    }
    onClickSprint(id) {
        this._router.navigate(['/app/sprint/sprint', id]);
    }

    active(record) {
        console.log('Active', record);
        // this.message.confirm(
        //     this.l('Bạn có muốn đóng sprint này không ? Nếu đóng các issue chưa hoàn thành của sprint sẽ chuyển thành backlog', record.issueCode),
        //     this.l('AreYouSure'),
        //     (isConfirmed) => {
        //         if (isConfirmed) {
        //              this._sprintService.activeSprint(record.id).subscribe(result => {
        //     this.getAll();
        //     this.notify.success('Active thanh cong');
        // });
        //         }
        //     }
        // );

        this._sprintService.activeSprint(record.id).subscribe(result => {
            this.getAll();
            this.notify.success('Active thanh cong');
        });
    }

    close(record) {
        console.log('close', record);
        this.message.confirm(
            this.l('Bạn có muốn đóng sprint này không ? Nếu đóng các issue chưa hoàn thành của sprint sẽ chuyển thành backlog', record.issueCode),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._sprintService.closeSprint(record.id).subscribe(result => {
                        this.getAll();
                        this.notify.success('close thanh cong');
                    });
                }
            }
        );
    }

    cancel(record) {
        console.log('cancel', record);
        this.message.confirm(
            this.l('Bạn có muốn hủy sprint này không ? Nếu hủy các issue chưa hoàn thành của sprint sẽ chuyển thành backlog', record.issueCode),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._sprintService.cancelSprint(record.id).subscribe(result => {
                        this.getAll();
                        this.notify.success('cancel thanh cong');
                    });
                }
            }
        );
    }
}
