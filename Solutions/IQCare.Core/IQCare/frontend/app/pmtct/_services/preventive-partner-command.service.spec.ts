import { TestBed, inject } from '@angular/core/testing';

import { PreventivePartnerCommandService } from './preventive-partner-command.service';

describe('PreventivePartnerCommandService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PreventivePartnerCommandService]
    });
  });

  it('should be created', inject([PreventivePartnerCommandService], (service: PreventivePartnerCommandService) => {
    expect(service).toBeTruthy();
  }));
});
