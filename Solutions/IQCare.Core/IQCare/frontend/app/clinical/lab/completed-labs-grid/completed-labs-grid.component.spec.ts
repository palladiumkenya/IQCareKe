import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedLabsGridComponent } from './completed-labs-grid.component';

describe('CompletedLabsGridComponent', () => {
  let component: CompletedLabsGridComponent;
  let fixture: ComponentFixture<CompletedLabsGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompletedLabsGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompletedLabsGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
