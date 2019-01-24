import { TestBed, inject } from '@angular/core/testing';

import { PersonRegistrationService } from './person-registration.service';

describe('PersonRegistrationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonRegistrationService]
    });
  });

  it('should be created', inject([PersonRegistrationService], (service: PersonRegistrationService) => {
    expect(service).toBeTruthy();
  }));
});
