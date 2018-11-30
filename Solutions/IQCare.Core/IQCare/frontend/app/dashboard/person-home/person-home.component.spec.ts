import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonHomeComponent } from './person-home.component';

describe('PersonHomeComponent', () => {
  let component: PersonHomeComponent;
  let fixture: ComponentFixture<PersonHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
