import { inject, Injectable } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/app-config.token';
import { LibraryRestService } from './library-rest.service';
import { LibraryGraphqlService } from './library-graphql.service';
import { Observable } from 'rxjs';
import { Library } from '../models/library.model';
import { LibraryDataService } from './library-data.service';
import { LibraryRequest } from '../models/library.request';

@Injectable({
  providedIn: 'root',
})
export class LibraryService {
  private readonly config = inject(APP_CONFIG);
  private readonly rest = inject(LibraryRestService);
  private readonly graphql = inject(LibraryGraphqlService);

  private get impl(): LibraryDataService {
    return this.config.useGraphQL ? this.graphql : this.rest;
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
}
