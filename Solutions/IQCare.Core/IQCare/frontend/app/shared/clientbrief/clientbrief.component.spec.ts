import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientbriefComponent } from './clientbrief.component';

describe('ClientbriefComponent', () => {
  let component: ClientbriefComponent;
  let fixture: ComponentFixture<ClientbriefComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientbriefComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientbriefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
