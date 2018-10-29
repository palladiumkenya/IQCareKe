import { TestBed, inject } from '@angular/core/testing';

import { VisitOptionsResolverService } from './visit-options-resolver.service';

describe('VisitOptionsResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VisitOptionsResolverService]
    });
  });

  it('should be created', inject([VisitOptionsResolverService], (service: VisitOptionsResolverService) => {
    expect(service).toBeTruthy();
  }));
});
