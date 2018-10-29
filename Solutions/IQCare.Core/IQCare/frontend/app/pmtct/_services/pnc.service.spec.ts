import { TestBed, inject } from '@angular/core/testing';

import { PncService } from './pnc.service';

describe('PncService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PncService]
    });
  });

  it('should be created', inject([PncService], (service: PncService) => {
    expect(service).toBeTruthy();
  }));
});
