import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTriageComponent } from './add-triage.component';

describe('AddTriageComponent', () => {
  let component: AddTriageComponent;
  let fixture: ComponentFixture<AddTriageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddTriageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTriageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
