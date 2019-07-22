import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PregnancyOutcomeComponent } from './pregnancy-outcome.component';

describe('PregnancyOutcomeComponent', () => {
  let component: PregnancyOutcomeComponent;
  let fixture: ComponentFixture<PregnancyOutcomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PregnancyOutcomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PregnancyOutcomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
