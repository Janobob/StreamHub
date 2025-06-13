import { provideEffects } from '@ngrx/effects';
import { provideState } from '@ngrx/store';
import { LibraryEffects } from './store/library.effects';
import { libraryReducer } from './store/library.reducer';

export const provideLibraryFeature = () => [
  provideState('library', libraryReducer),
  provideEffects([LibraryEffects]),
];
