import { TestBed, inject } from '@angular/core/testing';

import { MaternityService } from './maternity.service';

describe('MaternityService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MaternityService]
    });
  });

  it('should be created', inject([MaternityService], (service: MaternityService) => {
    expect(service).toBeTruthy();
  }));
});
