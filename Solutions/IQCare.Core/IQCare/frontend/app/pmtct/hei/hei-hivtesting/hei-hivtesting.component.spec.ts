import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiHivtestingComponent } from './hei-hivtesting.component';

describe('HeiHivtestingComponent', () => {
  let component: HeiHivtestingComponent;
  let fixture: ComponentFixture<HeiHivtestingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiHivtestingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiHivtestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
