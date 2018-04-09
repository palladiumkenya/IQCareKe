import { TestBed, inject } from '@angular/core/testing';

import { AppStateService } from './appstate.service';

describe('AppstateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AppStateService]
    });
  });

  it('should be created', inject([AppStateService], (service: AppStateService) => {
    expect(service).toBeTruthy();
  }));
});
