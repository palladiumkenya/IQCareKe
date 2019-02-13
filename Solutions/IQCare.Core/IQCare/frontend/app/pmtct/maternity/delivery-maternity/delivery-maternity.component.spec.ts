import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryMaternityComponent } from './delivery-maternity.component';

describe('DeliveryMaternityComponent', () => {
  let component: DeliveryMaternityComponent;
  let fixture: ComponentFixture<DeliveryMaternityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryMaternityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryMaternityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
