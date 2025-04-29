import { NgModule } from '@angular/core';
import { LibraryRoutingModule } from './library-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { LibrarySidebarComponent } from './components/library-sidebar/library-sidebar.component';
import { provideLibraryFeature } from './library.providers';

@NgModule({
  imports: [SharedModule, LibraryRoutingModule, LibrarySidebarComponent],
  providers: [provideLibraryFeature()],
})
export class LibraryModule {}
