import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepHtsencountersgridComponent } from './prep-htsencountersgrid.component';

describe('PrepHtsencountersgridComponent', () => {
  let component: PrepHtsencountersgridComponent;
  let fixture: ComponentFixture<PrepHtsencountersgridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepHtsencountersgridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepHtsencountersgridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
