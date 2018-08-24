import { TestBed, inject } from '@angular/core/testing';

import { VisitDetailsService } from './visit-details.service';

describe('VisitDetailsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VisitDetailsService]
    });
  });

  it('should be created', inject([VisitDetailsService], (service: VisitDetailsService) => {
    expect(service).toBeTruthy();
  }));
});
