import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncPatienteducationComponent } from './pnc-patienteducation.component';

describe('PncPatienteducationComponent', () => {
  let component: PncPatienteducationComponent;
  let fixture: ComponentFixture<PncPatienteducationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncPatienteducationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncPatienteducationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
