import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncEncountersComponent } from './pnc-encounters.component';

describe('PncEncountersComponent', () => {
  let component: PncEncountersComponent;
  let fixture: ComponentFixture<PncEncountersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncEncountersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncEncountersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
