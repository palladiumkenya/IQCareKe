import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompleteLabOrderComponent } from './complete-lab-order.component';

describe('CompleteLabOrderComponent', () => {
  let component: CompleteLabOrderComponent;
  let fixture: ComponentFixture<CompleteLabOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompleteLabOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompleteLabOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
