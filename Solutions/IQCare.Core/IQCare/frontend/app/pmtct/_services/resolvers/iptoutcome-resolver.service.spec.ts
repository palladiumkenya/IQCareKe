import { TestBed, inject } from '@angular/core/testing';

import { IptoutcomeResolverService } from './iptoutcome-resolver.service';

describe('IptoutcomeResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IptoutcomeResolverService]
    });
  });

  it('should be created', inject([IptoutcomeResolverService], (service: IptoutcomeResolverService) => {
    expect(service).toBeTruthy();
  }));
});
