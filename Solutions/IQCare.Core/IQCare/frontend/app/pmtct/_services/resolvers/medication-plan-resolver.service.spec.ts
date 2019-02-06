import { TestBed, inject } from '@angular/core/testing';

import { MedicationPlanResolverService } from './medication-plan-resolver.service';

describe('MedicationPlanResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MedicationPlanResolverService]
    });
  });

  it('should be created', inject([MedicationPlanResolverService], (service: MedicationPlanResolverService) => {
    expect(service).toBeTruthy();
  }));
});
