import { TestBed, inject } from '@angular/core/testing';

import { ModuleManagerService } from './module-manager.service';

describe('ModuleManagerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ModuleManagerService]
    });
  });

  it('should be created', inject([ModuleManagerService], (service: ModuleManagerService) => {
    expect(service).toBeTruthy();
  }));
});
