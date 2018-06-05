import { TestBed, inject } from '@angular/core/testing';

import { ApploadService } from './appload.service';

describe('ApploadService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApploadService]
    });
  });

  it('should be created', inject([ApploadService], (service: ApploadService) => {
    expect(service).toBeTruthy();
  }));
});
