import { TestBed, inject } from '@angular/core/testing';

import { ReferralAppointmentCommandService } from './referral-appointment-command.service';

describe('ReferralAppointmentCommandService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReferralAppointmentCommandService]
    });
  });

  it('should be created', inject([ReferralAppointmentCommandService], (service: ReferralAppointmentCommandService) => {
    expect(service).toBeTruthy();
  }));
});
