import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {
  catchError,
  filter,
  map,
  mergeMap,
  of,
  tap,
  withLatestFrom,
} from 'rxjs';
import { LibraryService } from '../services/library.service';
import * as LibraryActions from './library.actions';
import { MessageService } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { selectAllLibraries } from './library.selectors';

@Injectable()
export class LibraryEffects {
  private actions$ = inject(Actions);
  private service = inject(LibraryService);
  private messageService = inject(MessageService);
  private translate = inject(TranslateService);
  private store = inject(Store);

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

  createLibrarySuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.createLibrarySuccess),
        tap(({ library }) => {
          this.messageService.clear();
          this.messageService.add({
            severity: 'success',
            summary: this.translate.instant(
              'libraries.messages.create.success.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.create.success.detail',
              { name: library.name }
            ),
            life: 3000,
          });
        })
      ),
    { dispatch: false }
  );

  createLibraryFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.createLibraryFailure),
        tap(() => {
          this.messageService.add({
            severity: 'error',
            summary: this.translate.instant(
              'libraries.messages.create.error.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.create.error.detail'
            ),
            life: 5000,
          });
        })
      ),
    { dispatch: false }
  );

  loadLibrariesFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.loadLibrariesFailure),
        tap(({ error }) => {
          const isConnectionError =
            !error || error?.status === 0 || error?.status === 503;
          const basePath = 'libraries.messages.load';

          this.messageService.add({
            severity: 'error',
            summary: this.translate.instant(
              `${basePath}.${
                isConnectionError ? 'backendUnavailable' : 'error'
              }.title`
            ),
            detail: this.translate.instant(
              `${basePath}.${
                isConnectionError ? 'backendUnavailable' : 'error'
              }.detail`
            ),
            life: 5000,
          });
        })
      ),
    { dispatch: false }
  );

  updateLibrarySuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.updateLibrarySuccess),
        tap(({ library }) => {
          this.messageService.clear();
          this.messageService.add({
            severity: 'success',
            summary: this.translate.instant(
              'libraries.messages.update.success.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.update.success.detail',
              { name: library.name }
            ),
            life: 3000,
          });
        })
      ),
    { dispatch: false }
  );

  updateLibraryFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.updateLibraryFailure),
        tap(() => {
          this.messageService.add({
            severity: 'error',
            summary: this.translate.instant(
              'libraries.messages.update.error.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.update.error.detail'
            ),
            life: 5000,
          });
        })
      ),
    { dispatch: false }
  );

  deleteLibrarySuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.deleteLibrarySuccess),
        tap(({ id }) => {
          this.messageService.clear();
          this.messageService.add({
            severity: 'success',
            summary: this.translate.instant(
              'libraries.messages.delete.success.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.delete.success.detail',
              { id }
            ),
            life: 3000,
          });
        })
      ),
    { dispatch: false }
  );

  deleteLibraryFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.deleteLibraryFailure),
        tap(() => {
          this.messageService.add({
            severity: 'error',
            summary: this.translate.instant(
              'libraries.messages.delete.error.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.delete.error.detail'
            ),
            life: 5000,
          });
        })
      ),
    { dispatch: false }
  );

  created$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.libraryCreated),
        tap(({ library }) => {
          this.messageService.add({
            severity: 'info',
            summary: this.translate.instant(
              'libraries.messages.create.extern.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.create.extern.detail',
              { name: library.name }
            ),
            life: 3000,
          });
        })
      ),
    { dispatch: false }
  );

  updated$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.libraryUpdated),
        tap(({ library }) => {
          this.messageService.add({
            severity: 'info',
            summary: this.translate.instant(
              'libraries.messages.update.extern.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.update.extern.detail',
              { name: library.name }
            ),
            life: 3000,
          });
        })
      ),
    { dispatch: false }
  );

  deleted$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LibraryActions.libraryDeleted),
        tap(({ id }) => {
          this.messageService.add({
            severity: 'info',
            summary: this.translate.instant(
              'libraries.messages.delete.extern.title'
            ),
            detail: this.translate.instant(
              'libraries.messages.delete.extern.detail',
              { id }
            ),
            life: 3000,
          });
        })
      ),
    { dispatch: false }
  );
}
