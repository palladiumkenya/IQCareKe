import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HivStatusComponent } from './hiv-status.component';

describe('HivStatusComponent', () => {
  let component: HivStatusComponent;
  let fixture: ComponentFixture<HivStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HivStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HivStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
