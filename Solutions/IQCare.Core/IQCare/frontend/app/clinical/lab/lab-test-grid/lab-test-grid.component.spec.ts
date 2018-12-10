import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LabTestGridComponent } from './lab-test-grid.component';

describe('LabTestGridComponent', () => {
  let component: LabTestGridComponent;
  let fixture: ComponentFixture<LabTestGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LabTestGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LabTestGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
