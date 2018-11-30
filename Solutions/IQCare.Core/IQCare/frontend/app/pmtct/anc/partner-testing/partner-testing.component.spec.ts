import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PartnerTestingComponent } from './partner-testing.component';

describe('PartnerTestingComponent', () => {
  let component: PartnerTestingComponent;
  let fixture: ComponentFixture<PartnerTestingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PartnerTestingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PartnerTestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
