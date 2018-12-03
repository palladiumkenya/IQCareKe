import { TestBed, inject } from '@angular/core/testing';

import { TriageService } from './triage.service';

describe('TriageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TriageService]
    });
  });

  it('should be created', inject([TriageService], (service: TriageService) => {
    expect(service).toBeTruthy();
  }));
});
