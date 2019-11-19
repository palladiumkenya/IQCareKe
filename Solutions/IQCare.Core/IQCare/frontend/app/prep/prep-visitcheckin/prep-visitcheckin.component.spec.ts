import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepVisitcheckinComponent } from './prep-visitcheckin.component';

describe('PrepVisitcheckinComponent', () => {
  let component: PrepVisitcheckinComponent;
  let fixture: ComponentFixture<PrepVisitcheckinComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepVisitcheckinComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepVisitcheckinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
