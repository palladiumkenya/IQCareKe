import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsComponent } from './hts.component';

describe('HtsComponent', () => {
  let component: HtsComponent;
  let fixture: ComponentFixture<HtsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
