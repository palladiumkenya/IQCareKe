import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AncClientMonitoringComponent } from './anc-client-monitoring.component';

describe('AncClientMonitoringComponent', () => {
  let component: AncClientMonitoringComponent;
  let fixture: ComponentFixture<AncClientMonitoringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AncClientMonitoringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AncClientMonitoringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
