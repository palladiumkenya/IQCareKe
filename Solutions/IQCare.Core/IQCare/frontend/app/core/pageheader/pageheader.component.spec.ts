import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageheaderComponent } from './pageheader.component';

describe('PageheaderComponent', () => {
  let component: PageheaderComponent;
  let fixture: ComponentFixture<PageheaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PageheaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageheaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
