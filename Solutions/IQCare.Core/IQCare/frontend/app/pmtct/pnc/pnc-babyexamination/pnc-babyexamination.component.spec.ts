import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncBabyexaminationComponent } from './pnc-babyexamination.component';

describe('PncBabyexaminationComponent', () => {
  let component: PncBabyexaminationComponent;
  let fixture: ComponentFixture<PncBabyexaminationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncBabyexaminationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncBabyexaminationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
