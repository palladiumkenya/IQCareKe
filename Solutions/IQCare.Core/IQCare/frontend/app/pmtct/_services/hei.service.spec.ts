import { TestBed, inject } from '@angular/core/testing';

import { HeiService } from './hei.service';

describe('HeiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HeiService]
    });
  });

  it('should be created', inject([HeiService], (service: HeiService) => {
    expect(service).toBeTruthy();
  }));
});
