import { TestBed, inject } from '@angular/core/testing';

import { AncService } from './anc.service';

describe('AncService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AncService]
    });
  });

  it('should be created', inject([AncService], (service: AncService) => {
    expect(service).toBeTruthy();
  }));
});
