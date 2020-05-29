import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { LazyLoadEvent } from 'primeng/api';
import { finalize } from 'rxjs/operators';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/primeng';
import { ProjectServiceProxy, ProjectListDto, IssueServiceProxy, IssueListDto, CommonAppserviceServiceProxy, ERPComboboxItem, ConfigViewDto, ConfigviewServiceProxy, TreeNode, TreeViewServiceProxy, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { CreateIssueComponent } from './create-issue/create-issue.component';
import { ConfigViewComponent } from './config-view/config-view.component';
import { CreateOrUpdateSprintComponent } from './create-or-update-sprint/create-or-update-sprint.component';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import { TreeDataHelperService } from '@shared/utils/tree-data-helper.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-workflow-management',
    templateUrl: './workflow-management.component.html',
    styleUrls: ['./workflow-management.component.css'],
    animations: [appModuleAnimation()]
})
export class WorkflowManagementComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateIssueComponent;
    @ViewChild('configViewModal', { static: true }) configViewModal: ConfigViewComponent;
    @ViewChild('createOrEditSprintModal', { static: true }) createOrEditSprintModal: CreateOrUpdateSprintComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    lstProject: ERPComboboxItem[] = [];
    lstMember: ERPComboboxItem[] = [];
    lstIssueType: ERPComboboxItem[] = [];
    lstStatus: ERPComboboxItem[] = [];
    cols: any[];
    // tslint:disable-next-line: member-ordering
    file: TreeNode[] = [];
    file1: TreeNode[] = [];

    config: ConfigViewDto = new ConfigViewDto();
    //Filters
    filterText = '';
    lstProjectId: number[] = [];
    lstAssigneeId: number[] = [];
    lstIssueTypeId: number[] = [];
    lstStatusId: number[] = [];
    treeData: any;
    constructor(
        injector: Injector,
        private _issueServiceProxy: IssueServiceProxy,
        private _fileDownloadService: FileDownloadService,
        private _commonService: CommonAppserviceServiceProxy,
        private _configViewService: ConfigviewServiceProxy,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _treeViewService: TreeViewServiceProxy,
        private _route: Router

    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.initForm();
        this.getTreeDataFromServer();
    }
    private getTreeDataFromServer(): void {
        this._treeViewService.getTreeViewIssue()
            .subscribe((result) => {
                console.log('result', result);
                this.treeData = this._arrayToTreeConverterService.createTree(result.nodeFlat,
                    'parentId',
                    'id',
                    null,
                    'children',
                    [
                        {
                            target: 'label',
                            targetFunction(item) {
                                return item.name;
                            }
                            // source: 'name',
                        }, {
                            target: 'expandedIcon',
                            value: 'fa fa-folder-open m--font-warning'
                        },
                        {
                            target: 'collapsedIcon',
                            value: 'fa fa-folder m--font-warning'
                        },
                        {
                            target: 'selectable',
                            value: true
                        }
                    ]);
                console.log('this.treeData', this.treeData);
            });
    }

    initForm() {
        this._commonService.getLookups('Member', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstMember = result;
        });
        this._commonService.getLookups('Project', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstProject = result;
        });
        this._commonService.getLookups('IssueTypes', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstIssueType = result;
        });
        this._commonService.getLookups('Status', this.appSession.tenantId, undefined).subscribe(result => {
            this.lstStatus = result;
        });
        this._configViewService.getId(this.appSession.userId).subscribe(result => {
            this.config = result;
        });
    }
    getAll(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._issueServiceProxy.getAll(
            this.lstProjectId,
            this.lstStatusId,
            this.lstAssigneeId,
            this.lstIssueTypeId,
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
    createSprint() {
        this.createOrEditSprintModal.show();
    }
    editProject(id) {
        this.createOrEditModal.show(id);
    }
    exportExcel(event?: LazyLoadEvent) {
        this._issueServiceProxy.getIssueForExcel(
            this.lstProjectId,
            this.lstStatusId,
            this.lstAssigneeId,
            this.lstIssueTypeId,
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
    configView() {
        this.configViewModal.show();
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
                return this.l('Resolve');
                break;
            case 4:
                return this.l('Closed');
                break;
            case 5:
                return this.l('ReOpened');
                break;
        }
    }
    clickSummary(id) {
        this._route.navigate(['/app/issue/management-issue', id]);
    }
}

