import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateSprintComponent } from './create-or-update-sprint.component';

describe('CreateOrUpdateSprintComponent', () => {
  let component: CreateOrUpdateSprintComponent;
  let fixture: ComponentFixture<CreateOrUpdateSprintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrUpdateSprintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrUpdateSprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
