import { TestBed, inject } from '@angular/core/testing';

import { PsmartService } from './psmart.service';

describe('PsmartService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PsmartService]
    });
  });

  it('should be created', inject([PsmartService], (service: PsmartService) => {
    expect(service).toBeTruthy();
  }));
});
