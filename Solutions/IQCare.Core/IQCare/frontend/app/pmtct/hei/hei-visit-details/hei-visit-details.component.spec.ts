import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiVisitDetailsComponent } from './hei-visit-details.component';

describe('HeiVisitDetailsComponent', () => {
  let component: HeiVisitDetailsComponent;
  let fixture: ComponentFixture<HeiVisitDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiVisitDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiVisitDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
