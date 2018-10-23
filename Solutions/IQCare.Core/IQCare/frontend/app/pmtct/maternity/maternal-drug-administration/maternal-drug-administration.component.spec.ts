import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternalDrugAdministrationComponent } from './maternal-drug-administration.component';

describe('MaternalDrugAdministrationComponent', () => {
  let component: MaternalDrugAdministrationComponent;
  let fixture: ComponentFixture<MaternalDrugAdministrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternalDrugAdministrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternalDrugAdministrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
