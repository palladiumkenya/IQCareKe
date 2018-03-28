import { TestBed, inject } from '@angular/core/testing';

import { PnstracingService } from './pnstracing.service';

describe('PnstracingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PnstracingService]
    });
  });

  it('should be created', inject([PnstracingService], (service: PnstracingService) => {
    expect(service).toBeTruthy();
  }));
});
