import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TbAssessmentComponent } from './tb-assessment.component';

describe('TbAssessmentComponent', () => {
  let component: TbAssessmentComponent;
  let fixture: ComponentFixture<TbAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TbAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TbAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
