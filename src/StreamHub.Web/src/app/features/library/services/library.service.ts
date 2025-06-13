import { inject, Injectable } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/app-config.token';
import { LibraryRestService } from './library-rest.service';
import { LibraryGraphqlService } from './library-graphql.service';
import { Observable } from 'rxjs';
import { Library } from '../models/library.model';
import { LibraryDataService } from './library-data.service';
import * as LibraryActions from '../store/library.actions';
import { Store } from '@ngrx/store';

@Injectable({
  providedIn: 'root',
})
export class LibraryService {
  private readonly config = inject(APP_CONFIG);
  private readonly rest = inject(LibraryRestService);
  private readonly graphql = inject(LibraryGraphqlService);
  private readonly store = inject(Store);

  private get impl(): LibraryDataService {
    return this.config.useGraphQL ? this.graphql : this.rest;
  }

  constructor() {
    this.created$.subscribe((lib) => {
      console.log('Library created', lib);
      this.store.dispatch(LibraryActions.libraryCreated({ library: lib }));
    });

    this.updated$.subscribe((lib) =>
      this.store.dispatch(LibraryActions.libraryUpdated({ library: lib }))
    );

    this.deleted$.subscribe((id) =>
      this.store.dispatch(LibraryActions.libraryDeleted({ id }))
    );
  }

  getAll(): Observable<Library[]> {
    return this.impl.getAll();
  }

  get(id: number): Observable<Library> {
    return this.impl.get(id);
  }

  create(library: Library): Observable<Library> {
    return this.impl.create(library);
  }

  update(library: Library): Observable<Library> {
    return this.impl.update(library);
  }

  delete(id: number): Observable<void> {
    return this.impl.delete(id);
  }

  readonly created$ = this.impl.created$;
  readonly updated$ = this.impl.updated$;
  readonly deleted$ = this.impl.deleted$;
}
