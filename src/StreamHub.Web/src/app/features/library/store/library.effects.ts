import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { LibraryService } from '../services/library.service';
import * as LibraryActions from './library.actions';

@Injectable()
export class LibraryEffects {
  private actions$ = inject(Actions);
  private service = inject(LibraryService);

  loadLibraries$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LibraryActions.loadLibraries),
      mergeMap(() =>
        this.service.getAll().pipe(
          map((libraries) =>
            LibraryActions.loadLibrariesSuccess({ libraries })
          ),
          catchError((error) =>
            of(LibraryActions.loadLibrariesFailure({ error: error.error }))
          )
        )
      )
    )
  );

  loadLibrary$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LibraryActions.loadLibrary),
      mergeMap(({ id }) =>
        this.service.get(id).pipe(
          map((library) => LibraryActions.loadLibrarySuccess({ library })),
          catchError((error) =>
            of(LibraryActions.loadLibraryFailure({ error: error.error }))
          )
        )
      )
    )
  );

  createLibrary$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LibraryActions.createLibrary),
      mergeMap(({ library }) =>
        this.service.create(library).pipe(
          map((result) =>
            LibraryActions.createLibrarySuccess({ library: result })
          ),
          catchError((error) =>
            of(LibraryActions.createLibraryFailure({ error: error.error }))
          )
        )
      )
    )
  );

  updateLibrary$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LibraryActions.updateLibrary),
      mergeMap(({ library }) =>
        this.service.update(library).pipe(
          map((result) =>
            LibraryActions.updateLibrarySuccess({ library: result })
          ),
          catchError((error) =>
            of(LibraryActions.updateLibraryFailure({ error: error.error }))
          )
        )
      )
    )
  );

  deleteLibrary$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LibraryActions.deleteLibrary),
      mergeMap(({ id }) =>
        this.service.delete(id).pipe(
          map(() => LibraryActions.deleteLibrarySuccess({ id })),
          catchError((error) =>
            of(LibraryActions.deleteLibraryFailure({ error: error.error }))
          )
        )
      )
    )
  );
}
