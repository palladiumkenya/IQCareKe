import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOtzFormComponent } from './view-otz-form.component';

describe('ViewOtzFormComponent', () => {
  let component: ViewOtzFormComponent;
  let fixture: ComponentFixture<ViewOtzFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewOtzFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewOtzFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
