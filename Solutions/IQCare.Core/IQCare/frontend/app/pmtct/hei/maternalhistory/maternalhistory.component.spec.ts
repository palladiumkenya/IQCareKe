import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternalhistoryComponent } from './maternalhistory.component';

describe('MaternalhistoryComponent', () => {
  let component: MaternalhistoryComponent;
  let fixture: ComponentFixture<MaternalhistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternalhistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternalhistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
