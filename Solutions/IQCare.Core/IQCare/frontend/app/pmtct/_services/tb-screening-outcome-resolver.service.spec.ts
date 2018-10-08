import { TestBed, inject } from '@angular/core/testing';

import { TbScreeningOutcomeResolverService } from './tb-screening-outcome-resolver.service';

describe('TbScreeningOutcomeResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TbScreeningOutcomeResolverService]
    });
  });

  it('should be created', inject([TbScreeningOutcomeResolverService], (service: TbScreeningOutcomeResolverService) => {
    expect(service).toBeTruthy();
  }));
});
