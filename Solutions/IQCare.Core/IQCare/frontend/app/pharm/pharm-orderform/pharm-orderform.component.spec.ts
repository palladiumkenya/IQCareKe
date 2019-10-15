import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmOrderformComponent } from './pharm-orderform.component';

describe('PharmOrderformComponent', () => {
  let component: PharmOrderformComponent;
  let fixture: ComponentFixture<PharmOrderformComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PharmOrderformComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmOrderformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
