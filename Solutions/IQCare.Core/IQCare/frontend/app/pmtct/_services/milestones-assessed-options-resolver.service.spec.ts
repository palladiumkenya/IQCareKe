import { TestBed, inject } from '@angular/core/testing';

import { MilestonesAssessedOptionsResolverService } from './milestones-assessed-options-resolver.service';

describe('MilestonesAssessedOptionsResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MilestonesAssessedOptionsResolverService]
    });
  });

  it('should be created', inject([MilestonesAssessedOptionsResolverService], (service: MilestonesAssessedOptionsResolverService) => {
    expect(service).toBeTruthy();
  }));
});
