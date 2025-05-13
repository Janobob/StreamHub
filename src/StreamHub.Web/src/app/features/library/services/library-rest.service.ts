import { inject, Injectable } from '@angular/core';
import { LibraryDataService } from './library-data.service';
import { map, Observable } from 'rxjs';
import { Library } from '../models/library.model';
import { HttpClient } from '@angular/common/http';
import { LibraryResponse } from '../models/library.response';
import { toModel, toRequest } from '../models/library.mapper';
import { LibraryRequest } from '../models/library.request';

@Injectable({
  providedIn: 'root',
})
export class LibraryRestService implements LibraryDataService {
  private readonly apiUrl = '/api/libraries';
  private readonly http = inject(HttpClient);

  getAll(): Observable<Library[]> {
    return this.http
      .get<LibraryResponse[]>(this.apiUrl)
      .pipe(map((res) => res.map(toModel)));
  }

  get(id: number): Observable<Library> {
    return this.http
      .get<LibraryResponse>(`${this.apiUrl}/${id}`)
      .pipe(map(toModel));
  }

  create(library: Library): Observable<Library> {
    return this.http
      .post<LibraryResponse>(this.apiUrl, toRequest(library))
      .pipe(map(toModel));
  }

  update(library: Library): Observable<Library> {
    return this.http
      .put<LibraryResponse>(`${this.apiUrl}/${library.id}`, toRequest(library))
      .pipe(map(toModel));
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
