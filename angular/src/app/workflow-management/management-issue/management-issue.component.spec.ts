import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagementIssueComponent } from './management-issue.component';

describe('ManagementIssueComponent', () => {
  let component: ManagementIssueComponent;
  let fixture: ComponentFixture<ManagementIssueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagementIssueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagementIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
