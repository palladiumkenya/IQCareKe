import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepMonthlyrefillComponent } from './prep-monthlyrefill.component';

describe('PrepMonthlyrefillComponent', () => {
  let component: PrepMonthlyrefillComponent;
  let fixture: ComponentFixture<PrepMonthlyrefillComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepMonthlyrefillComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepMonthlyrefillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
