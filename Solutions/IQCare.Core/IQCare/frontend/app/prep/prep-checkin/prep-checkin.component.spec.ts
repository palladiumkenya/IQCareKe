import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepCheckinComponent } from './prep-checkin.component';

describe('PrepCheckinComponent', () => {
  let component: PrepCheckinComponent;
  let fixture: ComponentFixture<PrepCheckinComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepCheckinComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepCheckinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
