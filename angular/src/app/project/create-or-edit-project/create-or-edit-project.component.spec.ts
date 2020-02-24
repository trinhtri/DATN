import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditProjectComponent } from './create-or-edit-project.component';

describe('CreateOrEditProjectComponent', () => {
  let component: CreateOrEditProjectComponent;
  let fixture: ComponentFixture<CreateOrEditProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrEditProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
