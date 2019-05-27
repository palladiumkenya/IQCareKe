import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepEncounterComponent } from './prep-encounter.component';

describe('PrepEncounterComponent', () => {
  let component: PrepEncounterComponent;
  let fixture: ComponentFixture<PrepEncounterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepEncounterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepEncounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
