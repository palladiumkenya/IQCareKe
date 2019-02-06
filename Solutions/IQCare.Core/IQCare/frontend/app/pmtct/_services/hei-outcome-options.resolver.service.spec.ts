import { TestBed } from '@angular/core/testing';

import { HeiOutcomeOptionsResolver } from './hei-outcome-options.resolver.service';

describe('HeiOutcomeOptionsResolver', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HeiOutcomeOptionsResolver = TestBed.get(HeiOutcomeOptionsResolver);
    expect(service).toBeTruthy();
  });
});
