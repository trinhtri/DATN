import { Component, OnInit, Input, Injector, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { IssueServiceProxy, CommentServiceProxy, CommentListDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { CreateOrEditCommentComponent } from './create-or-edit-comment/create-or-edit-comment.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css'],
  animations: [appModuleAnimation()]

})
export class CommentsComponent extends AppComponentBase implements OnInit {
 @Input('IssueId') IssueId: number;
 @ViewChild('CreatModel', {static: true}) CreatModel: CreateOrEditCommentComponent;
 lstComment: CommentListDto [] = [] ;
  constructor(  injector: Injector,
    private _commentServiceProxy: CommentServiceProxy,
    private _activeRouter: ActivatedRoute) {
      super(injector);
    }

  ngOnInit() {
    this.IssueId = this._activeRouter.snapshot.params['id'];
    this.getAll();
  }
  getAll() {
    this._commentServiceProxy.getAll(
     this.IssueId
  ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
      this.lstComment = result;
      console.log('this.lstComment', this.lstComment);
  });
  }
  Create(id) {
    this.CreatModel.show(id);
  }
}
