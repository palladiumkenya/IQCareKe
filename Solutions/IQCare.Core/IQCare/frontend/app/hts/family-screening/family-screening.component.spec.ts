import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FamilyScreeningComponent } from './family-screening.component';

describe('FamilyScreeningComponent', () => {
  let component: FamilyScreeningComponent;
  let fixture: ComponentFixture<FamilyScreeningComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FamilyScreeningComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FamilyScreeningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
