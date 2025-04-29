import { NgModule } from '@angular/core';
import { LibraryRoutingModule } from './library-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { LibrarySidebarComponent } from './components/library-sidebar/library-sidebar.component';
import { provideLibraryFeature } from './library.providers';
import { LibraryFormDialogComponent } from './components/dialogs/library-form-dialog/library-form-dialog.component';

@NgModule({
  imports: [
    SharedModule,
    LibraryRoutingModule,
    LibrarySidebarComponent,
    LibraryFormDialogComponent,
  ],
  providers: [provideLibraryFeature()],
})
export class LibraryModule {}
