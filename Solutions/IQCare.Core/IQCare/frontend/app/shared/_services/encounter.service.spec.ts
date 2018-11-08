import { TestBed, inject } from '@angular/core/testing';

import { EncounterService } from './encounter.service';

describe('EncounterService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EncounterService]
    });
  });

  it('should be created', inject([EncounterService], (service: EncounterService) => {
    expect(service).toBeTruthy();
  }));
});
