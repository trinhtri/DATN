import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateCommentDto, CommentServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-or-edit-comment',
  templateUrl: './create-or-edit-comment.component.html',
  styleUrls: ['./create-or-edit-comment.component.css']
})
export class CreateOrEditCommentComponent extends AppComponentBase implements OnInit {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  comment: CreateCommentDto = new CreateCommentDto();
  active = false;
  saving = false;
  issueId: number;
  constructor(injector: Injector,
    private _comentService: CommentServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
  }
  show(id?: number): void {
    this.issueId = id;
    console.log('IssueId..', id);
    this.active = true;
    this.modal.show();
    // if (id) {
    //   this._comentService.getId(id).subscribe(result => {
    //     this.comment = result;
    //   });
    // }
  }
  onShown(): void {
    document.getElementById('Discription').focus();
  }
  save(): void {
    this.comment.issue_Id = this.issueId;
    this.comment.employee_Id = this.appSession.userId;
    this.saving = true;
    if (this.comment.id) {
      this._comentService.update(this.comment)
        .pipe(finalize(() => { this.saving = false; }))
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close();
          this.modalSave.emit(null);
        });
    } else {
      this._comentService.create(this.comment)
        .pipe(finalize(() => { this.saving = false; }))
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close();
          this.modalSave.emit(null);
        });
    }
  }

  close(): void {
    this.comment = new CreateCommentDto();
    this.saving = false;
    this.active = false;
    this.modal.hide();
  }
}
