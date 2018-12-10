import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreventiveServicesComponent } from './preventive-services.component';

describe('PreventiveServicesComponent', () => {
  let component: PreventiveServicesComponent;
  let fixture: ComponentFixture<PreventiveServicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreventiveServicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreventiveServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
