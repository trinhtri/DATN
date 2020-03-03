import { Component, OnInit, Input, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { MemberServiceProxy, DocumentListDto, DocumentServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { finalize } from 'rxjs/operators';
import { CreateOrEditDocumentComponent } from './create-or-edit-document/create-or-edit-document.component';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css'],
  animations: [appModuleAnimation()]
})
export class DocumentComponent extends AppComponentBase implements OnInit {
  @Input() projectId: number;
  @ViewChild('createOrEditModal', {static: true}) createOrEditModal: CreateOrEditDocumentComponent;
  @ViewChild('dataTable', {static: true}) dataTable: Table;
  @ViewChild('paginator', {static: true}) paginator: Paginator;
    //Filters
  filterText = '';
  constructor(  injector: Injector,
    private _memberServiceProxy: MemberServiceProxy,
    private _documentService: DocumentServiceProxy,
    private _fileDownloadService: FileDownloadService,
    ) {
      super(injector);
    }

  ngOnInit() {
  }
  getAll(event?: LazyLoadEvent) {
    if (this.primengTableHelper.shouldResetPaging(event)) {
        this.paginator.changePage(0);

        return;
    }

    this.primengTableHelper.showLoadingIndicator();

    this._documentService.getAll(
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
editDocument (projectId, id) {
    this.createOrEditModal.show(projectId, id);
}
delete(dto: DocumentListDto): void {
this.message.confirm(
    this.l('MemberDeleteWarningMessage', dto.documentName),
    this.l('AreYouSure'),
    (isConfirmed) => {
        if (isConfirmed) {
            this._documentService.delete(dto.id)
                .subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
        }
    }
);
}
download(id) {
this._documentService.downloadTempAttachment(id).subscribe(result => {
  this._fileDownloadService.downloadDocument(result);
});
}
// exportExcel(event?: LazyLoadEvent) {
// this._memberServiceProxy.getMemberForExcel(
//     this.projectId,
//     this.filterText,
//     this.primengTableHelper.getSorting(this.dataTable),
//     this.primengTableHelper.getMaxResultCount(this.paginator, event),
//     this.primengTableHelper.getSkipCount(this.paginator, event)
// ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
// this._fileDownloadService.downloadTempFile(result);
// });
// }
reloadPage(): void {
this.paginator.changePage(this.paginator.getPage());
}
}
