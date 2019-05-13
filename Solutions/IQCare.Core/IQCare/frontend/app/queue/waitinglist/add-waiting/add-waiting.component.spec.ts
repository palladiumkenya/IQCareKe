import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWaitingComponent } from './add-waiting.component';

describe('AddWaitingComponent', () => {
  let component: AddWaitingComponent;
  let fixture: ComponentFixture<AddWaitingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddWaitingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddWaitingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
