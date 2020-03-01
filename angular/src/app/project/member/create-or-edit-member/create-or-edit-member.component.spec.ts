import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditMemberComponent } from './create-or-edit-member.component';

describe('CreateOrEditMemberComponent', () => {
  let component: CreateOrEditMemberComponent;
  let fixture: ComponentFixture<CreateOrEditMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrEditMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
