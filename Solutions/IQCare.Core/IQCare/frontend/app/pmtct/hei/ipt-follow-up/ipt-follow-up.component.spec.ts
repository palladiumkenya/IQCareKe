import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IptFollowUpComponent } from './ipt-follow-up.component';

describe('IptFollowUpComponent', () => {
  let component: IptFollowUpComponent;
  let fixture: ComponentFixture<IptFollowUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IptFollowUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IptFollowUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
