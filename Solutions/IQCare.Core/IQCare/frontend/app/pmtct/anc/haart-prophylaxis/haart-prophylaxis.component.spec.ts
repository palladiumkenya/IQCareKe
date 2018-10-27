import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HaartProphylaxisComponent } from './haart-prophylaxis.component';

describe('HaartProphylaxisComponent', () => {
  let component: HaartProphylaxisComponent;
  let fixture: ComponentFixture<HaartProphylaxisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HaartProphylaxisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HaartProphylaxisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
