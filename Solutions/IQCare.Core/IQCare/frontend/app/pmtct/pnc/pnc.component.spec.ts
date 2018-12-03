import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncComponent } from './pnc.component';

describe('PncComponent', () => {
  let component: PncComponent;
  let fixture: ComponentFixture<PncComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
