import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LabOrderTestResultsComponent } from './lab-order-test-results.component';

describe('LabOrderTestResultsComponent', () => {
  let component: LabOrderTestResultsComponent;
  let fixture: ComponentFixture<LabOrderTestResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LabOrderTestResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LabOrderTestResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
