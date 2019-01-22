import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TriageInfoGridComponent } from './triage-info-grid.component';

describe('TriageInfoGridComponent', () => {
  let component: TriageInfoGridComponent;
  let fixture: ComponentFixture<TriageInfoGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TriageInfoGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TriageInfoGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
