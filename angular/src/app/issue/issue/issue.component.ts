import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { ERPComboboxItem, ProjectServiceProxy, SprintServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CreateIssueComponent } from '../create-issue/create-issue.component';

@Component({
    selector: 'app-issue',
    templateUrl: './issue.component.html',
    styleUrls: ['./issue.component.css'],
    animations: [appModuleAnimation()]
})
export class IssueComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateIssueComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    //Filters
    filterText = '';
    advancedFiltersAreShown: any;
    checked = true;
    lstMember: ERPComboboxItem[] = [];
    lstProject: ERPComboboxItem[] = [];
    lstType = [
        { value: 1, displayText: this.l('NewFeature') },
        { value: 2, displayText: this.l('Improvement') },
        { value: 3, displayText: this.l('Bug') },
    ];
    lstStatus = [
        { value: 1, displayText: this.l('Open') },
        { value: 2, displayText: this.l('InProgress') },
        { value: 3, displayText: this.l('Resolved') },
        { value: 4, displayText: this.l('Compeleted') },
        { value: 5, displayText: this.l('ReOpened') },
    ];
    lstStatusId: number[] = [];
    lstProjectId: number[] = [];
    lstIssueTypeId: number[] = [];
    lstAssigneeId: number[] = [];

    constructor(
        injector: Injector,
        private _projectServiceProxy: ProjectServiceProxy,
        private _fileDownloadService: FileDownloadService,
        private _sprintService: SprintServiceProxy,
        private _issueService: IssueServiceProxy,
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
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }
        this.primengTableHelper.showLoadingIndicator();
        this._issueService.getAll(
            this.lstProjectId,
            this.lstStatusId,
            this.lstAssigneeId,
            this.lstIssueTypeId,
            this.filterText,
            2,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            console.log('list issue', this.primengTableHelper.records);
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
            this.l('SprintDeleteWarningMessage', dto.issueCode),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._issueService.delete(dto.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    exportExcel(event?: LazyLoadEvent) {
        this._issueService.getSprintForExcel(
            this.lstProjectId,
            this.lstStatusId,
            undefined,
            this.lstIssueTypeId,
            this.filterText,
            1,
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

    onClickIssue(id) {
        this._router.navigate(['/app/issue/management-issue', id]);
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

    getStatusName(id) {
        switch (id) {
            case 1:
                return this.l('Open');
                break;
            case 2:
                return this.l('InProgress');
                break;
            case 3:
                return this.l('Resolved');
                break;
            case 4:
                return this.l('Compeleted');
                break;
            case 5:
                return this.l('ReOpened');
                break;
        }
    }
}
