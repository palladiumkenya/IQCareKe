import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBabyDialogComponent } from './add-baby-dialog.component';

describe('AddBabyDialogComponent', () => {
  let component: AddBabyDialogComponent;
  let fixture: ComponentFixture<AddBabyDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddBabyDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddBabyDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
