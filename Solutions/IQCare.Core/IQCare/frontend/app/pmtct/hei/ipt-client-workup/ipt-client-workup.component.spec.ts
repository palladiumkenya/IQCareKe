import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IptClientWorkupComponent } from './ipt-client-workup.component';

describe('IptClientWorkupComponent', () => {
  let component: IptClientWorkupComponent;
  let fixture: ComponentFixture<IptClientWorkupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IptClientWorkupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IptClientWorkupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
