import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BirthInfoGridComponent } from './birth-info-grid.component';

describe('BirthInfoGridComponent', () => {
  let component: BirthInfoGridComponent;
  let fixture: ComponentFixture<BirthInfoGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BirthInfoGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BirthInfoGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
