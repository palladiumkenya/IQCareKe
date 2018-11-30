import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncHivtestingComponent } from './pnc-hivtesting.component';

describe('PncHivtestingComponent', () => {
  let component: PncHivtestingComponent;
  let fixture: ComponentFixture<PncHivtestingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncHivtestingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncHivtestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
