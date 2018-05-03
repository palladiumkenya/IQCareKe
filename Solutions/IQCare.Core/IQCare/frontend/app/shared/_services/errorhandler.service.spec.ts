import { TestBed, inject } from '@angular/core/testing';

import { ErrorhandlerService } from './errorhandler.service';

describe('ErrorhandlerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorhandlerService]
    });
  });

  it('should be created', inject([ErrorhandlerService], (service: ErrorhandlerService) => {
    expect(service).toBeTruthy();
  }));
});
