import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IssueOfUserComponent } from './issue-of-user.component';

describe('IssueOfUserComponent', () => {
  let component: IssueOfUserComponent;
  let fixture: ComponentFixture<IssueOfUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IssueOfUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IssueOfUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
