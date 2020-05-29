import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { MemberListDto, MemberServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { CreateOrEditProjectComponent } from '../create-or-edit-project/create-or-edit-project.component';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { finalize } from 'rxjs/operators';
import { CreateOrEditMemberComponent } from './create-or-edit-member/create-or-edit-member.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css'],
  animations: [appModuleAnimation()]
})
export class MemberComponent extends AppComponentBase implements OnInit {
  @Input() projectId: number;
  members: MemberListDto[] = [];
  @ViewChild('createOrEditModal', { static: true }) createOrEditModal: CreateOrEditMemberComponent;
  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('paginator', { static: true }) paginator: Paginator;
  //Filters
  filterText = '';
  constructor(injector: Injector,
    private _memberServiceProxy: MemberServiceProxy,
    private _fileDownloadService: FileDownloadService,
  ) {
    super(injector);
  }

  ngOnInit() {
    console.log('a', this.projectId);
  }
  getAll(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);

      return;
    }

    this.primengTableHelper.showLoadingIndicator();

    this._memberServiceProxy.getAll(
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
  createNew(projectId) {
    this.createOrEditModal.show(projectId);
  }
  editMember(projectId, id) {
    this.createOrEditModal.show(projectId, id);
  }
  delete(dto: MemberListDto): void {
    this.message.confirm(
      this.l('MemberDeleteWarningMessage', dto.employeeName),
      this.l('AreYouSure'),
      (isConfirmed) => {
        if (isConfirmed) {
          this._memberServiceProxy.delete(dto.id)
            .subscribe(() => {
              this.reloadPage();
              this.notify.success(this.l('SuccessfullyDeleted'));
            });
        }
      }
    );
  }
  exportExcel(event?: LazyLoadEvent) {
    this._memberServiceProxy.getMemberForExcel(
      this.projectId,
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
  getRoleName(id) {
    switch (id) {
      case 1:
        return this.l('Manager');
        break;
      case 2:
        return this.l('Dev');
        break;
      case 3:
        return this.l('Test');
        break;
    }
  }
}
