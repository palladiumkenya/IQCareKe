import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FamilyTracingListComponent } from './family-tracing-list.component';

describe('FamilyTracingListComponent', () => {
  let component: FamilyTracingListComponent;
  let fixture: ComponentFixture<FamilyTracingListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FamilyTracingListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FamilyTracingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
