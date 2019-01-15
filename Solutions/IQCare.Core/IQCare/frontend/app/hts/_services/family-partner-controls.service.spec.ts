import { TestBed } from '@angular/core/testing';

import { FamilyPartnerControlsService } from './family-partner-controls.service';

describe('FamilyPartnerControlsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FamilyPartnerControlsService = TestBed.get(FamilyPartnerControlsService);
    expect(service).toBeTruthy();
  });
});
