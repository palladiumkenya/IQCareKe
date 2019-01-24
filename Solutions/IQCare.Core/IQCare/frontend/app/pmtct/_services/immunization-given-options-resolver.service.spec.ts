import { TestBed, inject } from '@angular/core/testing';

import { ImmunizationGivenOptionsResolverService } from './immunization-given-options-resolver.service';

describe('ImmunizationGivenOptionsResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ImmunizationGivenOptionsResolverService]
    });
  });

  it('should be created', inject([ImmunizationGivenOptionsResolverService], (service: ImmunizationGivenOptionsResolverService) => {
    expect(service).toBeTruthy();
  }));
});
