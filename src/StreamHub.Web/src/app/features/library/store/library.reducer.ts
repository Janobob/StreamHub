import { on, createReducer } from '@ngrx/store';
import * as LibraryActions from './library.actions';
import { ProblemDetails } from '../../../core/models/problem-details.model';
import { Library } from '../models/library.model';

export interface LibraryState {
  libraries: Library[];
  selectedLibrary: Library | null;
  loading: boolean;
  error: ProblemDetails | null;
}

export const initialState: LibraryState = {
  libraries: [],
  selectedLibrary: null,
  loading: false,
  error: null,
};

export const libraryReducer = createReducer(
  initialState,

  // --- Load all ---
  on(LibraryActions.loadLibraries, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),
  on(LibraryActions.loadLibrariesSuccess, (state, { libraries }) => ({
    ...state,
    libraries,
    loading: false,
  })),
  on(LibraryActions.loadLibrariesFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
  })),

  // --- Load one ---
  on(LibraryActions.loadLibrary, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),
  on(LibraryActions.loadLibrarySuccess, (state, { library }) => ({
    ...state,
    selectedLibrary: library,
    loading: false,
  })),
  on(LibraryActions.loadLibraryFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
  })),

  // --- Create ---
  on(LibraryActions.createLibrarySuccess, (state, { library }) => ({
    ...state,
    libraries: [...state.libraries, library],
  })),
  on(LibraryActions.createLibraryFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // --- Update ---
  on(LibraryActions.updateLibrarySuccess, (state, { library }) => ({
    ...state,
    libraries: state.libraries.map((l) => (l.id === library.id ? library : l)),
  })),
  on(LibraryActions.updateLibraryFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // --- Delete ---
  on(LibraryActions.deleteLibrarySuccess, (state, { id }) => ({
    ...state,
    libraries: state.libraries.filter((l) => l.id !== id),
  })),
  on(LibraryActions.deleteLibraryFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // --- Events ---
  on(LibraryActions.libraryCreated, (state, { library }) => ({
    ...state,
    libraries: [...state.libraries, library],
  })),
  on(LibraryActions.libraryUpdated, (state, { library }) => ({
    ...state,
    libraries: state.libraries.map((l) => (l.id === library.id ? library : l)),
  })),
  on(LibraryActions.libraryDeleted, (state, { id }) => ({
    ...state,
    libraries: state.libraries.filter((l) => l.id !== id),
  }))
);
