import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditSprintComponent } from './create-or-edit-sprint.component';

describe('CreateOrEditSprintComponent', () => {
  let component: CreateOrEditSprintComponent;
  let fixture: ComponentFixture<CreateOrEditSprintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrEditSprintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditSprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
