import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncDrugadministrationComponent } from './pnc-drugadministration.component';

describe('PncDrugadministrationComponent', () => {
  let component: PncDrugadministrationComponent;
  let fixture: ComponentFixture<PncDrugadministrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncDrugadministrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncDrugadministrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
