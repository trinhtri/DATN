import { Component, OnInit, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { ERPComboboxItem, ProjectServiceProxy, SprintServiceProxy, IssueServiceProxy, CommonAppserviceServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CreateIssueComponent } from '../create-issue/create-issue.component';
import { PrimengTableHelper } from '@shared/helpers/PrimengTableHelper';
import { IssueOfUserComponent } from './issue-of-user/issue-of-user.component';

@Component({
    selector: 'app-issue',
    templateUrl: './issue.component.html',
    styleUrls: ['./issue.component.less'],
    animations: [appModuleAnimation()]
})
export class IssueComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateIssueComponent;
    @ViewChild('createOrEditBackLogModal', { static: true }) createOrEditBackLogModal: CreateIssueComponent;

    @ViewChild('IssueOfUserComponent', { static: true }) IssueOfUserComponent: IssueOfUserComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    primengTableHelperIssueActive = new PrimengTableHelper();
    primengTableHelperBackLog = new PrimengTableHelper();
    //Filters
    filterText = '';
    advancedFiltersAreShown: any;
    advancedFiltersAreShown1: any;
    checked = true;
    lstMember: ERPComboboxItem[] = [];
    lstSprint: ERPComboboxItem[] = [];
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
    lstSprintId: number[] = [];
    lstIssueTypeId: number[] = [];
    lstAssigneeId: number[] = [];

    lstStatusBackLogId: number[] = [];
    lstSprintBackLogId: number[] = [];
    lstIssueTypeBackLogId: number[] = [];
    lstAssigneeBackLogId: number[] = [];

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
        this._commonService.getLookups('Sprints', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstSprint = result;
        });
    }
    getAll(event?: LazyLoadEvent) {
        if (this.primengTableHelperIssueActive.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }
        this.primengTableHelperIssueActive.showLoadingIndicator();
        this._issueService.getAll(
            this.lstSprintId,
            this.lstStatusId,
            this.lstAssigneeId,
            this.lstIssueTypeId,
            this.filterText,
            this.primengTableHelperIssueActive.getSorting(this.dataTable),
            this.primengTableHelperIssueActive.getMaxResultCount(this.paginator, event),
            this.primengTableHelperIssueActive.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelperIssueActive.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelperIssueActive.totalRecordsCount = result.totalCount;
            this.primengTableHelperIssueActive.records = result.items;
            console.log('list issue 1', this.primengTableHelperIssueActive.records);
            this.primengTableHelperIssueActive.hideLoadingIndicator();
        });
    }

    getAllBackLock(event?: LazyLoadEvent) {
        console.log('getAllBackLock')
        if (this.primengTableHelperBackLog.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }
        this.primengTableHelperBackLog.showLoadingIndicator();
        this._issueService.getAllIssueBackLog(
            this.lstSprintBackLogId,
            this.lstStatusBackLogId,
            this.lstAssigneeBackLogId,
            this.lstIssueTypeBackLogId,
            this.filterText,
            this.primengTableHelperBackLog.getSorting(this.dataTable),
            this.primengTableHelperBackLog.getMaxResultCount(this.paginator, event),
            this.primengTableHelperBackLog.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelperBackLog.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelperBackLog.totalRecordsCount = result.totalCount;
            this.primengTableHelperBackLog.records = result.items;
            console.log('list issue', this.primengTableHelper.records);
            this.primengTableHelperBackLog.hideLoadingIndicator();
        });
    }

    getAllTask(event){
        console.log('getAllTask1')
        this.getAll(event);
        this.getAllBackLock(event);
        this.getAllOfIssue();
        location.reload();
    }
    createNew() {
        this.createOrEditModal.show();
    }
    createNewBackLog() {
        this.createOrEditBackLogModal.show();
    }
    editSprint(id) {
        this.createOrEditModal.show(id);
    }
    delete(dto): void {
        console.log(dto);
        this.message.confirm(
            this.l('SprintDeleteWarningMessage', dto.taskCode),
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
        this._issueService.getIssueActiveForExcel(
            this.lstSprintId,
            this.lstStatusId,
            undefined,
            this.lstIssueTypeId,
            this.filterText,
            this.primengTableHelperIssueActive.getSorting(this.dataTable),
            this.primengTableHelperIssueActive.getMaxResultCount(this.paginator, event),
            this.primengTableHelperIssueActive.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelperIssueActive.hideLoadingIndicator())).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportExcelBackLog(event?: LazyLoadEvent) {
        this._issueService.getIssueBackLogForExcel(
            this.lstSprintId,
            this.lstStatusId,
            undefined,
            this.lstIssueTypeId,
            this.filterText,
            this.primengTableHelperIssueActive.getSorting(this.dataTable),
            this.primengTableHelperIssueActive.getMaxResultCount(this.paginator, event),
            this.primengTableHelperIssueActive.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelperIssueActive.hideLoadingIndicator())).subscribe(result => {
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
    getAllOfIssue(){
       this.IssueOfUserComponent.getAll();
    }
}
