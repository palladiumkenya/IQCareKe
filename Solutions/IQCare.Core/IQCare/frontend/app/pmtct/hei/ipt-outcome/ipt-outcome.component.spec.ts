import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IptOutcomeComponent } from './ipt-outcome.component';

describe('IptOutcomeComponent', () => {
  let component: IptOutcomeComponent;
  let fixture: ComponentFixture<IptOutcomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IptOutcomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IptOutcomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
