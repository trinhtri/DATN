import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigViewComponent } from './config-view.component';

describe('ConfigViewComponent', () => {
  let component: ConfigViewComponent;
  let fixture: ComponentFixture<ConfigViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
