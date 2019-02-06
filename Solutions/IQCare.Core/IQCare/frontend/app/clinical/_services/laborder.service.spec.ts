import { TestBed, inject } from '@angular/core/testing';

import { LaborderService } from './laborder.service';

describe('LaborderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LaborderService]
    });
  });

  it('should be created', inject([LaborderService], (service: LaborderService) => {
    expect(service).toBeTruthy();
  }));
});
