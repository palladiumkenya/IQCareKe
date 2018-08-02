import { RecordsModule } from './records.module';

describe('RecordsModule', () => {
  let recordsModule: RecordsModule;

  beforeEach(() => {
    recordsModule = new RecordsModule();
  });

  it('should create an instance', () => {
    expect(recordsModule).toBeTruthy();
  });
});
