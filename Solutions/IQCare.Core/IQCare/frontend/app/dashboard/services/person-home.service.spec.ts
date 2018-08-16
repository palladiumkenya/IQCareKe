import { TestBed, inject } from '@angular/core/testing';

import { Services\personHomeService } from './services\person-home.service';

describe('Services\personHomeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Services\personHomeService]
    });
  });

  it('should be created', inject([Services\personHomeService], (service: Services\personHomeService) => {
    expect(service).toBeTruthy();
  }));
});
