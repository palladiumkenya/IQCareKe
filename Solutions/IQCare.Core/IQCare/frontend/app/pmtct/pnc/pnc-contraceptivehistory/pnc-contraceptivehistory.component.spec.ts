import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PncContraceptivehistoryComponent } from './pnc-contraceptivehistory.component';

describe('PncContraceptivehistoryComponent', () => {
  let component: PncContraceptivehistoryComponent;
  let fixture: ComponentFixture<PncContraceptivehistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PncContraceptivehistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PncContraceptivehistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
