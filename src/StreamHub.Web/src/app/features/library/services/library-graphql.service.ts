import { Injectable, inject } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { LibraryDataService } from './library-data.service';
import { Library } from '../models/library.model';
import { Observable, map } from 'rxjs';
import { LibraryResponse } from '../models/library.response';
import { toModel, toRequest } from '../models/library.mapper';

@Injectable({
  providedIn: 'root',
})
export class LibraryGraphqlService implements LibraryDataService {
  private readonly apollo = inject(Apollo);

  getAll(): Observable<Library[]> {
    return this.apollo
      .query<{ mediaLibraries: LibraryResponse[] }>({
        query: gql`
          query {
            mediaLibraries {
              id
              name
              description
              path
            }
          }
        `,
      })
      .pipe(map((res) => res.data.mediaLibraries.map(toModel)));
  }

  get(id: number): Observable<Library> {
    return this.apollo
      .query<{ mediaLibrary: LibraryResponse }>({
        query: gql`
          query ($id: Int!) {
            mediaLibrary(id: $id) {
              id
              name
              description
              path
            }
          }
        `,
        variables: { id },
      })
      .pipe(map((res) => toModel(res.data.mediaLibrary)));
  }

  create(library: Library): Observable<Library> {
    console.log('Creating library:', library);
    return this.apollo
      .mutate<{ addMediaLibrary: LibraryResponse }>({
        mutation: gql`
          mutation ($input: MediaLibraryRequestInput!) {
            addMediaLibrary(mediaLibraryRequest: $input) {
              id
              name
              description
              path
            }
          }
        `,
        variables: { input: toRequest(library) },
      })
      .pipe(map((res) => toModel(res.data!.addMediaLibrary)));
  }

  update(library: Library): Observable<Library> {
    return this.apollo
      .mutate<{ updateMediaLibrary: LibraryResponse }>({
        mutation: gql`
          mutation ($input: MediaLibraryRequestInput!) {
            updateMediaLibrary(mediaLibraryRequest: $input) {
              id
              name
              description
              path
            }
          }
        `,
        variables: { input: toRequest(library) },
      })
      .pipe(map((res) => toModel(res.data!.updateMediaLibrary)));
  }

  delete(id: number): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation ($id: Int!) {
            deleteMediaLibrary(id: $id)
          }
        `,
        variables: { id },
      })
      .pipe(map(() => void 0));
  }
}
