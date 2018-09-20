import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PnsformComponent } from './pnsform.component';

describe('PnsformComponent', () => {
  let component: PnsformComponent;
  let fixture: ComponentFixture<PnsformComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PnsformComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PnsformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
