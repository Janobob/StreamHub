import { Library } from '../models/library.model';
import { Observable } from 'rxjs';

export interface LibraryDataService {
  getAll(): Observable<Library[]>;
  get(id: number): Observable<Library>;
  create(library: Library): Observable<Library>;
  update(library: Library): Observable<Library>;
  delete(id: number): Observable<void>;
}
