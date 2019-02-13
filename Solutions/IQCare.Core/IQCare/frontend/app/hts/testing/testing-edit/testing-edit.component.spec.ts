import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestingEditComponent } from './testing-edit.component';

describe('TestingEditComponent', () => {
  let component: TestingEditComponent;
  let fixture: ComponentFixture<TestingEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestingEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestingEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
