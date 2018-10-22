import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncCervicalcancerscreeningComponent } from './pnc-cervicalcancerscreening.component';

describe('PncCervicalcancerscreeningComponent', () => {
  let component: PncCervicalcancerscreeningComponent;
  let fixture: ComponentFixture<PncCervicalcancerscreeningComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncCervicalcancerscreeningComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncCervicalcancerscreeningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
