import { TestBed, inject } from '@angular/core/testing';

import { CountyService } from './county.service';

describe('CountyService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CountyService]
    });
  });

  it('should be created', inject([CountyService], (service: CountyService) => {
    expect(service).toBeTruthy();
  }));
});
