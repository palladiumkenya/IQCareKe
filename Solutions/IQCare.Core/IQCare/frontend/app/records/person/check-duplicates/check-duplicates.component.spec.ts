import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckDuplicatesComponent } from './check-duplicates.component';

describe('CheckDuplicatesComponent', () => {
  let component: CheckDuplicatesComponent;
  let fixture: ComponentFixture<CheckDuplicatesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CheckDuplicatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckDuplicatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
