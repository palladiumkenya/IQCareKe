import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FamilyTracingComponent } from './family-tracing.component';

describe('FamilyTracingComponent', () => {
  let component: FamilyTracingComponent;
  let fixture: ComponentFixture<FamilyTracingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FamilyTracingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FamilyTracingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
