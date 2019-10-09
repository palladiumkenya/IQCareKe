import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmMainpageComponent } from './pharm-mainpage.component';

describe('PharmMainpageComponent', () => {
  let component: PharmMainpageComponent;
  let fixture: ComponentFixture<PharmMainpageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PharmMainpageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmMainpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
