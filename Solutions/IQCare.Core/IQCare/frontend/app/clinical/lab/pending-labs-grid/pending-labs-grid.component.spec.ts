import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingLabsGridComponent } from './pending-labs-grid.component';

describe('PendingLabsGridComponent', () => {
  let component: PendingLabsGridComponent;
  let fixture: ComponentFixture<PendingLabsGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PendingLabsGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PendingLabsGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
