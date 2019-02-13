import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternitEncounterComponent } from './maternity-encounter.component';

describe('MaternitEncounterComponent', () => {
  let component: MaternitEncounterComponent;
  let fixture: ComponentFixture<MaternitEncounterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternitEncounterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternitEncounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
