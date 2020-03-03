import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditDocumentComponent } from './create-or-edit-document.component';

describe('CreateOrEditDocumentComponent', () => {
  let component: CreateOrEditDocumentComponent;
  let fixture: ComponentFixture<CreateOrEditDocumentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrEditDocumentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditDocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
