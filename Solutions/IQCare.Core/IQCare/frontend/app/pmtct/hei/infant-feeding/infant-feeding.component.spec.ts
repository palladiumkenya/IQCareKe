import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InfantFeedingComponent } from './infant-feeding.component';

describe('InfantFeedingComponent', () => {
  let component: InfantFeedingComponent;
  let fixture: ComponentFixture<InfantFeedingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InfantFeedingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InfantFeedingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
