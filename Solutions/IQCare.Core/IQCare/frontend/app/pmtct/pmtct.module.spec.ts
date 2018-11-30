import { PmtctModule } from './pmtct.module';

describe('PmtctModule', () => {
  let pmtctModule: PmtctModule;

  beforeEach(() => {
    pmtctModule = new PmtctModule();
  });

  it('should create an instance', () => {
    expect(pmtctModule).toBeTruthy();
  });
});
