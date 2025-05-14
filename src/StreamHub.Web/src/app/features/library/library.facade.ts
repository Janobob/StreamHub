import { Injectable, inject, signal } from '@angular/core';
import { Store } from '@ngrx/store';
import * as LibraryActions from './store/library.actions';
import * as LibrarySelectors from './store/library.selectors';
import { Library } from './models/library.model';
import { Actions, ofType } from '@ngrx/effects';

@Injectable({ providedIn: 'root' })
export class LibraryFacade {
  private readonly store = inject(Store);
  private readonly actions$ = inject(Actions);

  readonly libraries = this.store.selectSignal(
    LibrarySelectors.selectAllLibraries
  );
  readonly selectedLibrary = this.store.selectSignal(
    LibrarySelectors.selectSelectedLibrary
  );
  readonly loading = this.store.selectSignal(
    LibrarySelectors.selectLibraryLoading
  );
  readonly loadingType = this.store.selectSignal(
    LibrarySelectors.selectLibraryLoadingType
  );
  readonly error = this.store.selectSignal(LibrarySelectors.selectLibraryError);
  readonly createdSuccess = signal(false);
  readonly updatedSuccess = signal(false);
  readonly deletedSuccess = signal(false);

  constructor() {
    this.actions$
      .pipe(ofType(LibraryActions.createLibrarySuccess))
      .subscribe(() => {
        this.createdSuccess.set(true);
      });

    this.actions$
      .pipe(ofType(LibraryActions.updateLibrarySuccess))
      .subscribe(() => {
        this.updatedSuccess.set(true);
      });

    this.actions$
      .pipe(ofType(LibraryActions.deleteLibrarySuccess))
      .subscribe(() => {
        this.deletedSuccess.set(true);
      });
  }

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

  resetCreatedSuccess() {
    this.createdSuccess.set(false);
  }

  resetUpdatedSuccess() {
    this.updatedSuccess.set(false);
  }

  resetDeletedSuccess() {
    this.deletedSuccess.set(false);
  }
}
