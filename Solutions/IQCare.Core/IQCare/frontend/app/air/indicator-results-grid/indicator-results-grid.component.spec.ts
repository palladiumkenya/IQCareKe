import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IndicatorResultsGridComponent } from './indicator-results-grid.component';

describe('IndicatorResulsGridComponent', () => {
  let component: IndicatorResultsGridComponent;
  let fixture: ComponentFixture<IndicatorResultsGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IndicatorResultsGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IndicatorResultsGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
