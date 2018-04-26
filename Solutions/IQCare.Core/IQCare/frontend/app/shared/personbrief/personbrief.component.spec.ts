import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonbriefComponent } from './personbrief.component';

describe('PersonbriefComponent', () => {
  let component: PersonbriefComponent;
  let fixture: ComponentFixture<PersonbriefComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonbriefComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonbriefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
