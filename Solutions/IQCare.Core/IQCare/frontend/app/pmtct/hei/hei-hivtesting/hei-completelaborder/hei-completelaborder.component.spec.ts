import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiCompletelaborderComponent } from './hei-completelaborder.component';

describe('HeiCompletelaborderComponent', () => {
  let component: HeiCompletelaborderComponent;
  let fixture: ComponentFixture<HeiCompletelaborderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiCompletelaborderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiCompletelaborderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
