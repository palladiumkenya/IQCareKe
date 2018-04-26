import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkageComponent } from './linkage.component';

describe('LinkageComponent', () => {
  let component: LinkageComponent;
  let fixture: ComponentFixture<LinkageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
