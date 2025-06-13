import { inject, Injectable } from '@angular/core';
import { LibraryDataService } from './library-data.service';
import { map, Observable, Subject } from 'rxjs';
import { Library } from '../models/library.model';
import { HttpClient } from '@angular/common/http';
import { LibraryResponse } from '../models/library.response';
import * as signalR from '@microsoft/signalr';
import { toModel, toRequest } from '../models/library.mapper';

@Injectable({
  providedIn: 'root',
})
export class LibraryRestService implements LibraryDataService {
  private readonly apiUrl = '/api/libraries';
  private readonly http = inject(HttpClient);

  private hubConnection!: signalR.HubConnection;
  private readonly createdSubject = new Subject<Library>();
  created$ = this.createdSubject.asObservable();
  private readonly updatedSubject = new Subject<Library>();
  updated$ = this.updatedSubject.asObservable();
  private readonly deletedSubject = new Subject<number>();
  deleted$ = this.deletedSubject.asObservable();

  constructor() {
    this.startSignalR();
  }

  private startSignalR() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/hubs/libraries')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('libraryCreated', (library: LibraryResponse) => {
      this.createdSubject.next(toModel(library));
    });
    this.hubConnection.on('libraryUpdated', (library: LibraryResponse) => {
      this.updatedSubject.next(toModel(library));
    });
    this.hubConnection.on('libraryDeleted', (id: number) => {
      this.deletedSubject.next(id);
    });

    this.hubConnection
      .start()
      .catch((err) =>
        console.error('Error establishing SignalR connection', err)
      );
  }

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
