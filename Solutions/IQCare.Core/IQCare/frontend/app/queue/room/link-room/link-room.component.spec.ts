import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkRoomComponent } from './link-room.component';

describe('LinkRoomComponent', () => {
  let component: LinkRoomComponent;
  let fixture: ComponentFixture<LinkRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkRoomComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
