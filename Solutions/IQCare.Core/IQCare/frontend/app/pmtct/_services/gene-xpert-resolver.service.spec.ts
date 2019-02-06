import { TestBed, inject } from '@angular/core/testing';

import { GeneXpertResolverService } from './gene-xpert-resolver.service';

describe('GeneXpertResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GeneXpertResolverService]
    });
  });

  it('should be created', inject([GeneXpertResolverService], (service: GeneXpertResolverService) => {
    expect(service).toBeTruthy();
  }));
});
