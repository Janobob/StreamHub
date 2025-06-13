import { TestBed } from '@angular/core/testing';
import { LibraryRestService } from './library-rest.service';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('Service: LibraryRest', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        LibraryRestService,
      ],
    });
  });

  it('should be created', () => {
    const service = TestBed.inject(LibraryRestService);
    expect(service).toBeTruthy();
  });
});
