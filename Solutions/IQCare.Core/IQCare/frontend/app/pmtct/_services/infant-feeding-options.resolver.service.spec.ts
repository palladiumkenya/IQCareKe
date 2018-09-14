import { TestBed } from '@angular/core/testing';

import { InfantFeedingOptions.ResolverService } from './infant-feeding-options.resolver.service';

describe('InfantFeedingOptions.ResolverService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InfantFeedingOptions.ResolverService = TestBed.get(InfantFeedingOptions.ResolverService);
    expect(service).toBeTruthy();
  });
});
