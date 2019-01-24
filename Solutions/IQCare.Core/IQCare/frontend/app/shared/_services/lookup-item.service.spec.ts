import { TestBed, inject } from '@angular/core/testing';

import { LookupItemService } from './lookup-item.service';

describe('LookupItemService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LookupItemService]
    });
  });

  it('should be created', inject([LookupItemService], (service: LookupItemService) => {
    expect(service).toBeTruthy();
  }));
});
