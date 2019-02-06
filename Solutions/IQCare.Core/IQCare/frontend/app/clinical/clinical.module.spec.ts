import { ClinicalModule } from './clinical.module';

describe('ClinicalModule', () => {
  let clinicalModule: ClinicalModule;

  beforeEach(() => {
    clinicalModule = new ClinicalModule();
  });

  it('should create an instance', () => {
    expect(clinicalModule).toBeTruthy();
  });
});
