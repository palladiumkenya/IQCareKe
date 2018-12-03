import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HivtestingmodalComponent } from './hivtestingmodal.component';

describe('HivtestingmodalComponent', () => {
  let component: HivtestingmodalComponent;
  let fixture: ComponentFixture<HivtestingmodalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HivtestingmodalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HivtestingmodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
