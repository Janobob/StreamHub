/* tslint:disable:no-unused-variable */

import { TestBed, inject } from '@angular/core/testing';
import { ApolloTestingModule } from 'apollo-angular/testing';
import { LibraryGraphqlService } from './library-graphql.service';

describe('Service: LibraryGraphql', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ApolloTestingModule],
      providers: [LibraryGraphqlService],
    });
  });

  it('should ...', inject(
    [LibraryGraphqlService],
    (service: LibraryGraphqlService) => {
      expect(service).toBeTruthy();
    }
  ));
});
