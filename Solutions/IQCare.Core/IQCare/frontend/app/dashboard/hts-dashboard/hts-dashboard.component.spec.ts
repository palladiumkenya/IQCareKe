import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsDashboardComponent } from './hts-dashboard.component';

describe('HtsDashboardComponent', () => {
  let component: HtsDashboardComponent;
  let fixture: ComponentFixture<HtsDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
