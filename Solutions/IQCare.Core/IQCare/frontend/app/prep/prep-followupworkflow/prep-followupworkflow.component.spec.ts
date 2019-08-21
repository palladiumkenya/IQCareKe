import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepFollowupworkflowComponent } from './prep-followupworkflow.component';

describe('PrepFollowupworkflowComponent', () => {
  let component: PrepFollowupworkflowComponent;
  let fixture: ComponentFixture<PrepFollowupworkflowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepFollowupworkflowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepFollowupworkflowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
