import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdverseEventsTableComponent } from './adverse-events-table.component';

describe('AdverseEventsTableComponent', () => {
  let component: AdverseEventsTableComponent;
  let fixture: ComponentFixture<AdverseEventsTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdverseEventsTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdverseEventsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
