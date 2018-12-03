import { TestBed, inject } from '@angular/core/testing';

import { ChestXrayResolverService } from './chest-xray-resolver.service';

describe('ChestXrayResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChestXrayResolverService]
    });
  });

  it('should be created', inject([ChestXrayResolverService], (service: ChestXrayResolverService) => {
    expect(service).toBeTruthy();
  }));
});
