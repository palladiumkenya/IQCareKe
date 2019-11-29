import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OvcEncounterComponent } from './ovc-encounter.component';

describe('OvcEncounterComponent', () => {
  let component: OvcEncounterComponent;
  let fixture: ComponentFixture<OvcEncounterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OvcEncounterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OvcEncounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
