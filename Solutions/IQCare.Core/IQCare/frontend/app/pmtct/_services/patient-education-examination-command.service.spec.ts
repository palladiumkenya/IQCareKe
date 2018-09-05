import { TestBed, inject } from '@angular/core/testing';

import { PatientEducationExaminationCommandService } from './patient-education-examination-command.service';

describe('PatientEducationExaminationCommandService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PatientEducationExaminationCommandService]
    });
  });

  it('should be created', inject([PatientEducationExaminationCommandService], (service: PatientEducationExaminationCommandService) => {
    expect(service).toBeTruthy();
  }));
});
