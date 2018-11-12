import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AncHivtestingComponent } from './anc-hivtesting.component';

describe('AncHivtestingComponent', () => {
  let component: AncHivtestingComponent;
  let fixture: ComponentFixture<AncHivtestingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AncHivtestingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AncHivtestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
