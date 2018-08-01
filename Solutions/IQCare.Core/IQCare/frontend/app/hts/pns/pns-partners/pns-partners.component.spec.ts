import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PnsPartnersComponent } from './pns-partners.component';

describe('PnsPartnersComponent', () => {
  let component: PnsPartnersComponent;
  let fixture: ComponentFixture<PnsPartnersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PnsPartnersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PnsPartnersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
