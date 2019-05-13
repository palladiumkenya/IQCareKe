import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewWaitinglistComponent } from './view-waitinglist.component';

describe('ViewWaitinglistComponent', () => {
  let component: ViewWaitinglistComponent;
  let fixture: ComponentFixture<ViewWaitinglistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewWaitinglistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewWaitinglistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
