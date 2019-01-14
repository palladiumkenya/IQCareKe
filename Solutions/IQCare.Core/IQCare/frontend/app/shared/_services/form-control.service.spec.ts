import { TestBed, inject } from '@angular/core/testing';

import { FormControlService } from './form-control.service';

describe('FormControlService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FormControlService]
    });
  });

  it('should be created', inject([FormControlService], (service: FormControlService) => {
    expect(service).toBeTruthy();
  }));
});
