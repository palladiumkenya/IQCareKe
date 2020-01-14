import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OtzCareendingComponent } from './otz-careending.component';

describe('OtzCareendingComponent', () => {
  let component: OtzCareendingComponent;
  let fixture: ComponentFixture<OtzCareendingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OtzCareendingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OtzCareendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
