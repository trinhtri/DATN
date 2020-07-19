import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoryStatusIssueComponent } from './history-status-issue.component';

describe('HistoryStatusIssueComponent', () => {
  let component: HistoryStatusIssueComponent;
  let fixture: ComponentFixture<HistoryStatusIssueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoryStatusIssueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoryStatusIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
