import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncPartnertestingComponent } from './pnc-partnertesting.component';

describe('PncPartnertestingComponent', () => {
  let component: PncPartnertestingComponent;
  let fixture: ComponentFixture<PncPartnertestingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncPartnertestingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncPartnertestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
