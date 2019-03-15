import { TestBed } from '@angular/core/testing';

import { IndicatorService } from './indicator.service';

describe('IndicatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IndicatorService = TestBed.get(IndicatorService);
    expect(service).toBeTruthy();
  });
});
