import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncMaternalhistoryComponent } from './pnc-maternalhistory.component';

describe('PncMaternalhistoryComponent', () => {
  let component: PncMaternalhistoryComponent;
  let fixture: ComponentFixture<PncMaternalhistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncMaternalhistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncMaternalhistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
