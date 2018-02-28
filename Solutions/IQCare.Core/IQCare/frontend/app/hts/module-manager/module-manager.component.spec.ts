import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleManagerComponent } from './module-manager.component';

describe('ModuleManagerComponent', () => {
  let component: ModuleManagerComponent;
  let fixture: ComponentFixture<ModuleManagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModuleManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModuleManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
