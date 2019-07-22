import { TestBed } from '@angular/core/testing';

import { PrepService } from './prep.service';

describe('PrepService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PrepService = TestBed.get(PrepService);
    expect(service).toBeTruthy();
  });
});
