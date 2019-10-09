import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatconfirmdialogComponent } from './matconfirmdialog.component';

describe('MatconfirmdialogComponent', () => {
  let component: MatconfirmdialogComponent;
  let fixture: ComponentFixture<MatconfirmdialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatconfirmdialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatconfirmdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
