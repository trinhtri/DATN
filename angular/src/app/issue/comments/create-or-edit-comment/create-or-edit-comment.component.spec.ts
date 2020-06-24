import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditCommentComponent } from './create-or-edit-comment.component';

describe('CreateOrEditCommentComponent', () => {
  let component: CreateOrEditCommentComponent;
  let fixture: ComponentFixture<CreateOrEditCommentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrEditCommentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
