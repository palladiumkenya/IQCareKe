import { TestBed, inject } from '@angular/core/testing';

import { SputumSmearResolverService } from './sputum-smear-resolver.service';

describe('SputumSmearResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SputumSmearResolverService]
    });
  });

  it('should be created', inject([SputumSmearResolverService], (service: SputumSmearResolverService) => {
    expect(service).toBeTruthy();
  }));
});
