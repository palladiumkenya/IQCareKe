import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiMessagesComponent } from './hei-messages.component';

describe('HeiMessagesComponent', () => {
  let component: HeiMessagesComponent;
  let fixture: ComponentFixture<HeiMessagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiMessagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiMessagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
