import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternityHivTestComponent } from './maternity-hiv-test.component';

describe('MaternityHivTestComponent', () => {
  let component: MaternityHivTestComponent;
  let fixture: ComponentFixture<MaternityHivTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternityHivTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternityHivTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
