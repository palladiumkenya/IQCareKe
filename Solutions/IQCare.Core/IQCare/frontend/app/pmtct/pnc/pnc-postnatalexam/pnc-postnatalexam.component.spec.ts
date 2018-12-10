import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncPostnatalexamComponent } from './pnc-postnatalexam.component';

describe('PncPostnatalexamComponent', () => {
  let component: PncPostnatalexamComponent;
  let fixture: ComponentFixture<PncPostnatalexamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncPostnatalexamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncPostnatalexamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
