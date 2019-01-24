import { TestBed, inject } from '@angular/core/testing';

import { MedicationResolverService } from './medication-resolver.service';

describe('MedicationResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MedicationResolverService]
    });
  });

  it('should be created', inject([MedicationResolverService], (service: MedicationResolverService) => {
    expect(service).toBeTruthy();
  }));
});
