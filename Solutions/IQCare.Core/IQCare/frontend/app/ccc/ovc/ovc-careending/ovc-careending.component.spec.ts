import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OvcCareendingComponent } from './ovc-careending.component';

describe('OvcCareendingComponent', () => {
  let component: OvcCareendingComponent;
  let fixture: ComponentFixture<OvcCareendingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OvcCareendingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OvcCareendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
