import { TestBed, inject } from '@angular/core/testing';

import { ClientMonitoringCommandService } from './client-monitoring-command.service';

describe('ClientMonitoringCommandService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientMonitoringCommandService]
    });
  });

  it('should be created', inject([ClientMonitoringCommandService], (service: ClientMonitoringCommandService) => {
    expect(service).toBeTruthy();
  }));
});
