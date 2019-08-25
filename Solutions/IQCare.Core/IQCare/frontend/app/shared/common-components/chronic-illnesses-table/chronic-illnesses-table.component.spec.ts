import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChronicIllnessesTableComponent } from './chronic-illnesses-table.component';

describe('ChronicIllnessesTableComponent', () => {
  let component: ChronicIllnessesTableComponent;
  let fixture: ComponentFixture<ChronicIllnessesTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChronicIllnessesTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChronicIllnessesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
