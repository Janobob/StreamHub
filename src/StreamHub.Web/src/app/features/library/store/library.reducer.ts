import { on, createReducer } from '@ngrx/store';
import * as LibraryActions from './library.actions';
import { ProblemDetails } from '../../../core/models/problem-details.model';
import { Library } from '../models/library.model';

export type LoadingType = 'all' | 'one' | 'create' | 'update' | 'delete' | null;
export const LoadingTypes = {
  All: 'all',
  One: 'one',
  Create: 'create',
  Update: 'update',
  Delete: 'delete',
  None: null,
} as const;

export interface LibraryState {
  libraries: Library[];
  selectedLibrary: Library | null;
  loading: boolean;
  loadingType: LoadingType;
  error: ProblemDetails | null;
}

export const initialState: LibraryState = {
  libraries: [],
  selectedLibrary: null,
  loading: false,
  loadingType: LoadingTypes.None,
  error: null,
};

export const libraryReducer = createReducer(
  initialState,

  // --- Load all ---
  on(LibraryActions.loadLibraries, (state) => ({
    ...state,
    loading: true,
    loadingType: LoadingTypes.All,
    error: null,
  })),
  on(LibraryActions.loadLibrariesSuccess, (state, { libraries }) => ({
    ...state,
    libraries,
    loading: false,
    loadingType: LoadingTypes.None,
  })),
  on(LibraryActions.loadLibrariesFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
    loadingType: LoadingTypes.None,
  })),

  // --- Load one ---
  on(LibraryActions.loadLibrary, (state) => ({
    ...state,
    loading: true,
    loadingType: LoadingTypes.One,
    error: null,
  })),
  on(LibraryActions.loadLibrarySuccess, (state, { library }) => ({
    ...state,
    selectedLibrary: library,
    loading: false,
    loadingType: LoadingTypes.None,
  })),
  on(LibraryActions.loadLibraryFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
    loadingType: LoadingTypes.None,
  })),

  // --- Create ---
  on(LibraryActions.createLibrary, (state) => ({
    ...state,
    loading: true,
    loadingType: LoadingTypes.Create,
  })),
  on(LibraryActions.createLibrarySuccess, (state, { library }) => ({
    ...state,
    libraries: state.libraries.some((l) => l.id === library.id)
      ? state.libraries
      : [...state.libraries, library],
    loading: false,
    loadingType: LoadingTypes.None,
  })),
  on(LibraryActions.createLibraryFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
    loadingType: LoadingTypes.None,
  })),

  // --- Update ---
  on(LibraryActions.updateLibrary, (state) => ({
    ...state,
    loading: true,
    loadingType: LoadingTypes.Update,
  })),
  on(LibraryActions.updateLibrarySuccess, (state, { library }) => ({
    ...state,
    libraries: state.libraries.map((l) => (l.id === library.id ? library : l)),
    loading: false,
    loadingType: LoadingTypes.None,
  })),
  on(LibraryActions.updateLibraryFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
    loadingType: LoadingTypes.None,
  })),

  // --- Delete ---
  on(LibraryActions.deleteLibrary, (state) => ({
    ...state,
    loading: true,
    loadingType: LoadingTypes.Delete,
  })),
  on(LibraryActions.deleteLibrarySuccess, (state, { id }) => ({
    ...state,
    libraries: state.libraries.filter((l) => l.id !== id),
    loading: false,
    loadingType: LoadingTypes.None,
  })),
  on(LibraryActions.deleteLibraryFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
    loadingType: LoadingTypes.None,
  })),

  // --- Events ---
  on(LibraryActions.libraryCreated, (state, { library }) => ({
    ...state,
    libraries: state.libraries.some((l) => l.id === library.id)
      ? state.libraries
      : [...state.libraries, library],
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
