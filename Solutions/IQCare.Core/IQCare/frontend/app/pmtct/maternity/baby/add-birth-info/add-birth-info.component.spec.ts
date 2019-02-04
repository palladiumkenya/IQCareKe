import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBirthInfoComponent } from './add-birth-info.component';

describe('AddBirthInfoComponent', () => {
  let component: AddBirthInfoComponent;
  let fixture: ComponentFixture<AddBirthInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddBirthInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddBirthInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
