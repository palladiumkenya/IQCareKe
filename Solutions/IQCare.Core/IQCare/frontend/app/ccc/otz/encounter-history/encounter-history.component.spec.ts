import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EncounterHistoryComponent } from './encounter-history.component';

describe('EncounterHistoryComponent', () => {
  let component: EncounterHistoryComponent;
  let fixture: ComponentFixture<EncounterHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EncounterHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EncounterHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
