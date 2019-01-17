import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLabResultComponent } from './add-lab-result.component';

describe('AddLabResultComponent', () => {
  let component: AddLabResultComponent;
  let fixture: ComponentFixture<AddLabResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddLabResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddLabResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
