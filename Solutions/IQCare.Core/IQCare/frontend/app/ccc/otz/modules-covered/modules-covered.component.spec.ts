import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModulesCoveredComponent } from './modules-covered.component';

describe('ModulesCoveredComponent', () => {
  let component: ModulesCoveredComponent;
  let fixture: ComponentFixture<ModulesCoveredComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModulesCoveredComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModulesCoveredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
