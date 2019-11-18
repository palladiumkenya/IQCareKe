import { TestBed } from '@angular/core/testing';

import { OtzService } from './otz.service';

describe('OtzService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OtzService = TestBed.get(OtzService);
    expect(service).toBeTruthy();
  });
});
