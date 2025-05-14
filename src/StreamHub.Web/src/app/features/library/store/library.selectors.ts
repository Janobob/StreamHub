import { createFeatureSelector, createSelector } from '@ngrx/store';
import { LibraryState } from './library.reducer';

export const selectLibraryState =
  createFeatureSelector<LibraryState>('library');

export const selectAllLibraries = createSelector(
  selectLibraryState,
  (state) => state.libraries
);

export const selectSelectedLibrary = createSelector(
  selectLibraryState,
  (state) => state.selectedLibrary
);

export const selectLibraryLoadingType = createSelector(
  selectLibraryState,
  (state) => state.loadingType
);

export const selectLibraryLoading = createSelector(
  selectLibraryState,
  (state) => state.loading
);

export const selectLibraryError = createSelector(
  selectLibraryState,
  (state) => state.error
);
