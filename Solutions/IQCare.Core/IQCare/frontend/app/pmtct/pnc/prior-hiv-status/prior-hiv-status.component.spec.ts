import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PriorHivStatusComponent } from './prior-hiv-status.component';

describe('PriorHivStatusComponent', () => {
  let component: PriorHivStatusComponent;
  let fixture: ComponentFixture<PriorHivStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PriorHivStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PriorHivStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
