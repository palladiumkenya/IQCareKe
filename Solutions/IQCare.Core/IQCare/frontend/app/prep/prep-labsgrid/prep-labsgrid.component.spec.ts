import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepLabsgridComponent } from './prep-labsgrid.component';

describe('PrepLabsgridComponent', () => {
  let component: PrepLabsgridComponent;
  let fixture: ComponentFixture<PrepLabsgridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepLabsgridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepLabsgridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
