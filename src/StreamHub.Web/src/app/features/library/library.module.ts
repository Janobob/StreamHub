import { NgModule } from '@angular/core';
import { LibraryRoutingModule } from './library-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { LibrarySidebarComponent } from './components/library-sidebar/library-sidebar.component';
import { provideLibraryFeature } from './library.providers';
import { LibraryFormDialogComponent } from './components/dialogs/library-form-dialog/library-form-dialog.component';
import { LibraryDeleteDialogComponent } from './components/dialogs/library-delete-dialog/library-delete-dialog.component';

@NgModule({
  imports: [
    SharedModule,
    LibraryRoutingModule,
    LibrarySidebarComponent,
    LibraryFormDialogComponent,
    LibraryDeleteDialogComponent,
  ],
  providers: [provideLibraryFeature()],
})
export class LibraryModule {}
