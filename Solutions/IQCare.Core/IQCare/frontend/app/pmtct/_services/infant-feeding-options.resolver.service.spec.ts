import { TestBed } from '@angular/core/testing';

import { InfantFeedingOptionsResolver } from './infant-feeding-options.resolver.service';

describe('InfantFeedingOptionsResolver', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InfantFeedingOptionsResolver = TestBed.get(InfantFeedingOptionsResolver);
    expect(service).toBeTruthy();
  });
});
