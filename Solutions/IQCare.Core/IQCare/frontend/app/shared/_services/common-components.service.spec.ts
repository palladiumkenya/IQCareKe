import { TestBed } from '@angular/core/testing';

import { CommonComponentsService } from './common-components.service';

describe('CommonComponentsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommonComponentsService = TestBed.get(CommonComponentsService);
    expect(service).toBeTruthy();
  });
});
