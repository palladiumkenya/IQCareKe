import { TestBed } from '@angular/core/testing';

import { FacilityService } from './facility.service';

describe('FacilityService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FacilityService = TestBed.get(FacilityService);
    expect(service).toBeTruthy();
  });
});
