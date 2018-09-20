import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestdialogComponent } from './testdialog.component';

describe('TestdialogComponent', () => {
  let component: TestdialogComponent;
  let fixture: ComponentFixture<TestdialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestdialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
