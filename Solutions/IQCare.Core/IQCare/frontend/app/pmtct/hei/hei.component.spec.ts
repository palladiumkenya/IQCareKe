import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiComponent } from './hei.component';

describe('HeiComponent', () => {
  let component: HeiComponent;
  let fixture: ComponentFixture<HeiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
