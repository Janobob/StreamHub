import { Injectable, inject } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { LibraryDataService } from './library-data.service';
import { Library } from '../models/library.model';
import { Observable, map } from 'rxjs';
import { LibraryResponse } from '../models/library.response';
import { toModel, toRequest } from '../models/ibrary.mapper';

@Injectable({
  providedIn: 'root',
})
export class LibraryGraphqlService implements LibraryDataService {
  private readonly apollo = inject(Apollo);

  getAll(): Observable<Library[]> {
    return this.apollo
      .query<{ getAllMediaLibrariesAsync: LibraryResponse[] }>({
        query: gql`
          query {
            getAllMediaLibrariesAsync {
              id
              name
              description
              path
            }
          }
        `,
      })
      .pipe(map((res) => res.data.getAllMediaLibrariesAsync.map(toModel)));
  }

  get(id: number): Observable<Library> {
    return this.apollo
      .query<{ getMediaLibraryAsync: LibraryResponse }>({
        query: gql`
          query ($id: Int!) {
            getMediaLibraryAsync(id: $id) {
              id
              name
              description
              path
            }
          }
        `,
        variables: { id },
      })
      .pipe(map((res) => toModel(res.data.getMediaLibraryAsync)));
  }

  create(library: Library): Observable<Library> {
    return this.apollo
      .mutate<{ addMediaLibraryAsync: LibraryResponse }>({
        mutation: gql`
          mutation ($input: MediaLibraryRequestInput!) {
            addMediaLibraryAsync(mediaLibraryRequest: $input) {
              id
              name
              description
              path
            }
          }
        `,
        variables: { input: toRequest(library) },
      })
      .pipe(map((res) => toModel(res.data!.addMediaLibraryAsync)));
  }

  update(library: Library): Observable<Library> {
    return this.apollo
      .mutate<{ updateMediaLibraryAsync: LibraryResponse }>({
        mutation: gql`
          mutation ($input: MediaLibraryRequestInput!) {
            updateMediaLibraryAsync(mediaLibraryRequest: $input) {
              id
              name
              description
              path
            }
          }
        `,
        variables: { input: toRequest(library) },
      })
      .pipe(map((res) => toModel(res.data!.updateMediaLibraryAsync)));
  }

  delete(id: number): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation ($id: Int!) {
            deleteMediaLibraryAsync(id: $id)
          }
        `,
        variables: { id },
      })
      .pipe(map(() => void 0));
  }
}
