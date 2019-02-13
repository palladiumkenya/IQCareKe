import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AntenatalProfileComponent } from './antenatal-profile.component';

describe('AntenatalProfileComponent', () => {
  let component: AntenatalProfileComponent;
  let fixture: ComponentFixture<AntenatalProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AntenatalProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AntenatalProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
