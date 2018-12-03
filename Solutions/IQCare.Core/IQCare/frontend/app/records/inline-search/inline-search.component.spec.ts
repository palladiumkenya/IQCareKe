import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InlineSearchComponent } from './inline-search.component';

describe('InlineSearchComponent', () => {
  let component: InlineSearchComponent;
  let fixture: ComponentFixture<InlineSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InlineSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InlineSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
