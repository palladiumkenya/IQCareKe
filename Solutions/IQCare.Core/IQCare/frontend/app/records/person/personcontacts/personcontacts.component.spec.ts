import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersoncontactsComponent } from './personcontacts.component';

describe('PersoncontactsComponent', () => {
  let component: PersoncontactsComponent;
  let fixture: ComponentFixture<PersoncontactsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersoncontactsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersoncontactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
