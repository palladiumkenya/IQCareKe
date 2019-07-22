import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FertilityIntentionComponent } from './fertility-intention.component';

describe('FertilityIntentionComponent', () => {
  let component: FertilityIntentionComponent;
  let fixture: ComponentFixture<FertilityIntentionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FertilityIntentionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FertilityIntentionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
