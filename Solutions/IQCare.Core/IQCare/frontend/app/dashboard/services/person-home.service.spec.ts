import { TestBed, inject } from '@angular/core/testing';
import {PersonHomeService} from './person-home.service';


describe('PersonHomeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonHomeService]
    });
  });

  it('should be created', inject([PersonHomeService], (service: PersonHomeService) => {
    expect(service).toBeTruthy();
  }));
});
