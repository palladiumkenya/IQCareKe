import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordsNavComponent  } from './nav.component';

describe('RecordsNavComponent ', () => {
  let component: RecordsNavComponent ;
  let fixture: ComponentFixture<RecordsNavComponent >;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecordsNavComponent  ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordsNavComponent );
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
