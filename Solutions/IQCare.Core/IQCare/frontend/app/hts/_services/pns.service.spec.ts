import { TestBed, inject } from '@angular/core/testing';

import { PnsService } from './pns.service';

describe('PnsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PnsService]
    });
  });

  it('should be created', inject([PnsService], (service: PnsService) => {
    expect(service).toBeTruthy();
  }));
});
