import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PnsTracingComponent } from './pnstracing.component';

describe('PnstracingComponent', () => {
  let component: PnsTracingComponent;
  let fixture: ComponentFixture<PnsTracingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PnsTracingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PnsTracingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
