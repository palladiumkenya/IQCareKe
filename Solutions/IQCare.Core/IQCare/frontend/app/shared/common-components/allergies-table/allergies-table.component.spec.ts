import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllergiesTableComponent } from './allergies-table.component';

describe('AllergiesTableComponent', () => {
  let component: AllergiesTableComponent;
  let fixture: ComponentFixture<AllergiesTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllergiesTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllergiesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
