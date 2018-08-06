import { TestBed, inject } from '@angular/core/testing';

import { AppLoadService  } from './appload.service';

describe('ApploadService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AppLoadService]
    });
  });

  it('should be created', inject([AppLoadService], (service: AppLoadService) => {
    expect(service).toBeTruthy();
  }));
});
