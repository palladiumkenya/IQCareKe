import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepCareendComponent } from './prep-careend.component';

describe('PrepCareendComponent', () => {
  let component: PrepCareendComponent;
  let fixture: ComponentFixture<PrepCareendComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepCareendComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepCareendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
