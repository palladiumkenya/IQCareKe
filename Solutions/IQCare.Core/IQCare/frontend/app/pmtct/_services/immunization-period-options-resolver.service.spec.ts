import { TestBed, inject } from '@angular/core/testing';

import { ImmunizationPeriodOptionsResolverService } from './immunization-period-options-resolver.service';

describe('ImmunizationPeriodOptionsResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ImmunizationPeriodOptionsResolverService]
    });
  });

  it('should be created', inject([ImmunizationPeriodOptionsResolverService], (service: ImmunizationPeriodOptionsResolverService) => {
    expect(service).toBeTruthy();
  }));
});
