import { Injectable, inject } from '@angular/core';
import { Store } from '@ngrx/store';
import * as LibraryActions from './store/library.actions';
import * as LibrarySelectors from './store/library.selectors';
import { Library } from './models/library.model';

@Injectable({ providedIn: 'root' })
export class LibraryFacade {
  private readonly store = inject(Store);

  readonly libraries = this.store.selectSignal(
    LibrarySelectors.selectAllLibraries
  );
  readonly selectedLibrary = this.store.selectSignal(
    LibrarySelectors.selectSelectedLibrary
  );
  readonly loading = this.store.selectSignal(
    LibrarySelectors.selectLibraryLoading
  );
  readonly error = this.store.selectSignal(LibrarySelectors.selectLibraryError);

  loadAll() {
    this.store.dispatch(LibraryActions.loadLibraries());
  }

  load(id: number) {
    this.store.dispatch(LibraryActions.loadLibrary({ id }));
  }

  create(library: Library) {
    this.store.dispatch(LibraryActions.createLibrary({ library }));
  }

  update(library: Library) {
    this.store.dispatch(LibraryActions.updateLibrary({ library }));
  }

  delete(id: number) {
    this.store.dispatch(LibraryActions.deleteLibrary({ id }));
  }
}
