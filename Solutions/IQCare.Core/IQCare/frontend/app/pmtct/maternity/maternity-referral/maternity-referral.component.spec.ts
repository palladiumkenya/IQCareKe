import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternityReferralComponent } from './maternity-referral.component';

describe('MaternityReferralComponent', () => {
  let component: MaternityReferralComponent;
  let fixture: ComponentFixture<MaternityReferralComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternityReferralComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternityReferralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
