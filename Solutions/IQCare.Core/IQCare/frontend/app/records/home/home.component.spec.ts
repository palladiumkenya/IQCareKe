import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordsHomeComponent } from './home.component';

describe('RecordsHomeComponent', () => {
  let component: RecordsHomeComponent;
  let fixture: ComponentFixture<RecordsHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecordsHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordsHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
