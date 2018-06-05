import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PsmartComponent } from './psmart.component';

describe('PsmartComponent', () => {
  let component: PsmartComponent;
  let fixture: ComponentFixture<PsmartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PsmartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PsmartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
