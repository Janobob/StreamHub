import { createAction, props } from '@ngrx/store';
import { Library } from '../models/library.model';
import { ProblemDetails } from '../../../core/models/problem-details.model';

export const loadLibraries = createAction('[Library] Load All');
export const loadLibrariesSuccess = createAction(
  '[Library] Load All Success',
  props<{ libraries: Library[] }>()
);
export const loadLibrariesFailure = createAction(
  '[Library] Load All Failure',
  props<{ error: ProblemDetails }>()
);

export const loadLibrary = createAction(
  '[Library] Load',
  props<{ id: number }>()
);
export const loadLibrarySuccess = createAction(
  '[Library] Load Success',
  props<{ library: Library }>()
);
export const loadLibraryFailure = createAction(
  '[Library] Load Failure',
  props<{ error: ProblemDetails }>()
);

export const createLibrary = createAction(
  '[Library] Create',
  props<{ library: Library }>()
);
export const createLibrarySuccess = createAction(
  '[Library] Create Success',
  props<{ library: Library }>()
);
export const createLibraryFailure = createAction(
  '[Library] Create Failure',
  props<{ error: ProblemDetails }>()
);

export const updateLibrary = createAction(
  '[Library] Update',
  props<{ library: Library }>()
);
export const updateLibrarySuccess = createAction(
  '[Library] Update Success',
  props<{ library: Library }>()
);
export const updateLibraryFailure = createAction(
  '[Library] Update Failure',
  props<{ error: ProblemDetails }>()
);

export const deleteLibrary = createAction(
  '[Library] Delete',
  props<{ id: number }>()
);
export const deleteLibrarySuccess = createAction(
  '[Library] Delete Success',
  props<{ id: number }>()
);
export const deleteLibraryFailure = createAction(
  '[Library] Delete Failure',
  props<{ error: ProblemDetails }>()
);

export const libraryCreated = createAction(
  '[Library] Created',
  props<{ library: Library }>()
);
export const libraryUpdated = createAction(
  '[Library] Updated',
  props<{ library: Library }>()
);
export const libraryDeleted = createAction(
  '[Library] Deleted',
  props<{ id: number }>()
);
