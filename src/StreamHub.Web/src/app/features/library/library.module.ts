import { NgModule } from '@angular/core';
import { LibraryRoutingModule } from './library-routing.module';
import { provideState } from '@ngrx/store';
import { libraryReducer } from './store/library.reducer';
import { provideEffects } from '@ngrx/effects';
import { LibraryEffects } from './store/library.effects';
import { SharedModule } from '../../shared/shared.module';
import { LibrarySidebarComponent } from './components/library-sidebar/library-sidebar.component';

@NgModule({
  imports: [SharedModule, LibraryRoutingModule, LibrarySidebarComponent],
  providers: [
    provideState('library', libraryReducer),
    provideEffects([LibraryEffects]),
  ],
})
export class LibraryModule {}
