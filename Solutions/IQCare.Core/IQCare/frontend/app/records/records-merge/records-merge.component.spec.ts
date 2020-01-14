import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordsMergeComponent } from './records-merge.component';

describe('RecordsMergeComponent', () => {
  let component: RecordsMergeComponent;
  let fixture: ComponentFixture<RecordsMergeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecordsMergeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordsMergeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
