import { TestBed, inject } from '@angular/core/testing';

import { LinkageReferralService } from './linkage-referral.service';

describe('LinkageReferralService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LinkageReferralService]
    });
  });

  it('should be created', inject([LinkageReferralService], (service: LinkageReferralService) => {
    expect(service).toBeTruthy();
  }));
});
