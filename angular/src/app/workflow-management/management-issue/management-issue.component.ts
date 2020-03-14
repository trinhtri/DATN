import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-management-issue',
  templateUrl: './management-issue.component.html',
  styleUrls: ['./management-issue.component.css']
})
export class ManagementIssueComponent extends AppComponentBase implements OnInit {
  // @ViewChild('createOrEditModal', {static: true}) createOrEditModal: CreateIssueComponent;

  constructor(injector: Injector) {
    super(injector
      );
  }

  ngOnInit() {
  }

}
