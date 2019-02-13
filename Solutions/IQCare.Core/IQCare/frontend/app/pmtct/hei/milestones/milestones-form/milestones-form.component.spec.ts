import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MilestonesFormComponent } from './milestones-form.component';

describe('MilestonesFormComponent', () => {
  let component: MilestonesFormComponent;
  let fixture: ComponentFixture<MilestonesFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MilestonesFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MilestonesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
