import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkageReferralComponent } from './linkage-referral.component';

describe('LinkageReferralComponent', () => {
  let component: LinkageReferralComponent;
  let fixture: ComponentFixture<LinkageReferralComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkageReferralComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkageReferralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
