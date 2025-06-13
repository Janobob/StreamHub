import { TestBed } from '@angular/core/testing';
import { LibraryService } from './library.service';
import { LibraryGraphqlService } from './library-graphql.service';
import { LibraryRestService } from './library-rest.service';
import { of } from 'rxjs';
import { APP_CONFIG } from '../../../core/config/app-config.token';

describe('LibraryService - useGraphQL = true', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        LibraryService,
        { provide: APP_CONFIG, useValue: { useGraphQL: true } },
        {
          provide: LibraryRestService,
          useValue: jasmine.createSpyObj('LibraryRestService', ['getAll']),
        },
        {
          provide: LibraryGraphqlService,
          useValue: jasmine.createSpyObj('LibraryGraphqlService', ['getAll']),
        },
      ],
    });
  });

  it('should call getAll on GraphQL service if enabled', () => {
    const gql = TestBed.inject(
      LibraryGraphqlService
    ) as jasmine.SpyObj<LibraryGraphqlService>;
    gql.getAll.and.returnValue(of([]));

    const service = TestBed.inject(LibraryService);
    service.getAll().subscribe();
    expect(gql.getAll).toHaveBeenCalled();
  });
});

describe('LibraryService - useGraphQL = false', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        LibraryService,
        { provide: APP_CONFIG, useValue: { useGraphQL: false } },
        {
          provide: LibraryRestService,
          useValue: jasmine.createSpyObj('LibraryRestService', ['getAll']),
        },
        {
          provide: LibraryGraphqlService,
          useValue: jasmine.createSpyObj('LibraryGraphqlService', ['getAll']),
        },
      ],
    });
  });

  it('should call getAll on REST service if disabled', () => {
    const rest = TestBed.inject(
      LibraryRestService
    ) as jasmine.SpyObj<LibraryRestService>;
    rest.getAll.and.returnValue(of([]));

    const service = TestBed.inject(LibraryService);
    service.getAll().subscribe();
    expect(rest.getAll).toHaveBeenCalled();
  });
});
