import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { HistoryStatusIssueListDto, HistoryStatusIssueServiceProxy } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-history-status-issue',
  templateUrl: './history-status-issue.component.html',
  styleUrls: ['./history-status-issue.component.css']
})
export class HistoryStatusIssueComponent extends AppComponentBase implements OnInit {

    @Input('IssueId') IssueId: number;
    lstHistory: HistoryStatusIssueListDto [] = [] ;
     constructor(  injector: Injector,
       private _historyIssueService: HistoryStatusIssueServiceProxy,
       private _activeRouter: ActivatedRoute) {
         super(injector);
       }

     ngOnInit() {
       this.IssueId = this._activeRouter.snapshot.params['id'];
       this.getAll();
     }
     getAll() {
       this._historyIssueService.getAll(
        this.IssueId
     ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
         this.lstHistory = result;
         console.log('this.lstComment', this.lstHistory);
     });

}
}
