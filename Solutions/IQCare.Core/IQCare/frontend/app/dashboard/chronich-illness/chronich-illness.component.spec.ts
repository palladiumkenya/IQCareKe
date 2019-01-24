import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChronichIllnessComponent } from './chronich-illness.component';

describe('ChronichIllnessComponent', () => {
  let component: ChronichIllnessComponent;
  let fixture: ComponentFixture<ChronichIllnessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChronichIllnessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChronichIllnessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
