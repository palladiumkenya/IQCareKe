import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MotherProfileComponent } from './mother-profile.component';

describe('MotherProfileComponent', () => {
  let component: MotherProfileComponent;
  let fixture: ComponentFixture<MotherProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MotherProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MotherProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
