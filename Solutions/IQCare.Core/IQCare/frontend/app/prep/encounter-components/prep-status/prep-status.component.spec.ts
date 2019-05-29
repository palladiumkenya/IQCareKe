import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepStatusComponent } from './prep-status.component';

describe('PrepStatusComponent', () => {
  let component: PrepStatusComponent;
  let fixture: ComponentFixture<PrepStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
