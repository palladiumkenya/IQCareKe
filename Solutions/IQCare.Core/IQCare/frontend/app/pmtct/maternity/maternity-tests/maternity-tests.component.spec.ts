import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternityTestsComponent } from './maternity-tests.component';

describe('MternityTestsComponent', () => {
  let component: MaternityTestsComponent;
  let fixture: ComponentFixture<MaternityTestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternityTestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternityTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
