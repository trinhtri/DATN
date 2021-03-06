import { Component, OnInit, ViewChild, EventEmitter, Output, Injector, ElementRef } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CreateDocumentDto, DocumentServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { FileUploader, FileUploaderOptions } from 'ng2-file-upload';
import { AppConsts } from '@shared/AppConsts';
import { IAjaxResponse } from 'abp-ng2-module/dist/src/abpHttpInterceptor';
import { TokenService } from 'abp-ng2-module/dist/src/auth/token.service';
import * as moment from 'moment';
@Component({
  selector: 'app-create-or-edit-document',
  templateUrl: './create-or-edit-document.component.html',
  styleUrls: ['./create-or-edit-document.component.css']
})
export class CreateOrEditDocumentComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
  @ViewChild('documentFileInput', {static: false}) documentFileInput: ElementRef;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  document: CreateDocumentDto = new CreateDocumentDto();
  active = false;
  saving = false;
  projectId: any;
  selectedFile: File;
  isSelectedFile = false;
  documentUploader: FileUploader;
  _uploaderOptions: FileUploaderOptions = {};
  constructor(injector: Injector,
    private _tokenService:  TokenService,
    private _documentService: DocumentServiceProxy
    ) {
    super(injector);
   }

  ngOnInit() {
  }

  show(projectId, id?: number): void {
    this.active = true;
    this.projectId = projectId;
    this.modal.show();
    if (id) {
    this._documentService.getId(id).subscribe( result => {
      this.document = result;
  });
}}
onShown(): void {
  document.getElementById('DocumentName').focus();
}
save(): void {
  this.saving = true;
  this.document.project_Id = this.projectId;
  this.document.uploadDate = moment(new Date());
  if (this.document.id) {
    this._documentService.update(this.document)
    .pipe(finalize(() => { this.saving = false; }))
    .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
    });
  } else {
    this._documentService.create(this.document)
    .pipe(finalize(() => { this.saving = false; }))
    .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
    });
  }
}

close(): void {
  this.projectId = null;
  this.document = new CreateDocumentDto();
  this.saving = false;
  this.active = false;
  this.modal.hide();
}
}
