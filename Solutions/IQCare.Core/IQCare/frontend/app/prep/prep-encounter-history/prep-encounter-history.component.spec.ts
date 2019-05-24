import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepEncounterHistoryComponent } from './prep-encounter-history.component';

describe('PrepEncounterHistoryComponent', () => {
  let component: PrepEncounterHistoryComponent;
  let fixture: ComponentFixture<PrepEncounterHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepEncounterHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepEncounterHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
