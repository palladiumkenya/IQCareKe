import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEncounterComponent } from './view-encounter.component';

describe('ViewEncounterComponent', () => {
  let component: ViewEncounterComponent;
  let fixture: ComponentFixture<ViewEncounterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewEncounterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEncounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
