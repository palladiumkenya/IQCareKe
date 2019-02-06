import { TestBed, inject } from '@angular/core/testing';

import { MilestonesStatusOptionsResolverService } from './milestones-status-options-resolver.service';

describe('MilestonesStatusOptionsResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MilestonesStatusOptionsResolverService]
    });
  });

  it('should be created', inject([MilestonesStatusOptionsResolverService], (service: MilestonesStatusOptionsResolverService) => {
    expect(service).toBeTruthy();
  }));
});
