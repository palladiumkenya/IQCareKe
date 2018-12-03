import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiOutcomeComponent } from './hei-outcome.component';

describe('HeiOutcomeComponent', () => {
  let component: HeiOutcomeComponent;
  let fixture: ComponentFixture<HeiOutcomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiOutcomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiOutcomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
