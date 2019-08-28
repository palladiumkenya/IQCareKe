import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepEncounterformlistComponent } from './prep-encounterformlist.component';

describe('PrepEncounterformlistComponent', () => {
  let component: PrepEncounterformlistComponent;
  let fixture: ComponentFixture<PrepEncounterformlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepEncounterformlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepEncounterformlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
