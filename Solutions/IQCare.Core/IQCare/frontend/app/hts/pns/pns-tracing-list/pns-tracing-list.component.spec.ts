import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PnsTracingListComponent } from './pns-tracing-list.component';

describe('PnsTracingListComponent', () => {
  let component: PnsTracingListComponent;
  let fixture: ComponentFixture<PnsTracingListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PnsTracingListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PnsTracingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
