import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIssueOfSprintComponent } from './create-issue-of-sprint.component';

describe('CreateIssueOfSprintComponent', () => {
  let component: CreateIssueOfSprintComponent;
  let fixture: ComponentFixture<CreateIssueOfSprintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateIssueOfSprintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIssueOfSprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
