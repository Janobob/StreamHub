import { Component, inject, ViewChild } from '@angular/core';
import { LibraryFacade } from '../../library.facade';
import { ButtonModule } from 'primeng/button';
import { SkeletonModule } from 'primeng/skeleton';
import { TranslateModule } from '@ngx-translate/core';
import { BadgeModule } from 'primeng/badge';
import { LibraryFormDialogComponent } from '../dialogs/library-form-dialog/library-form-dialog.component';
import { Library } from '../../models/library.model';
import { MessageService } from 'primeng/api';

@Component({
  standalone: true,
  selector: 'app-library-sidebar',
  templateUrl: './library-sidebar.component.html',
  styleUrls: ['./library-sidebar.component.css'],
  imports: [
    ButtonModule,
    SkeletonModule,
    BadgeModule,
    TranslateModule,
    LibraryFormDialogComponent,
  ],
})
export class LibrarySidebarComponent {
  private readonly facade = inject(LibraryFacade);
  readonly libraries = this.facade.libraries;
  readonly loading = this.facade.loading;

  @ViewChild(LibraryFormDialogComponent)
  libraryFormDialog!: LibraryFormDialogComponent;

  constructor() {
    this.facade.loadAll();
  }

  onAddLibrary() {
    this.libraryFormDialog.open();
  }

  onSaveLibrary(library: Library) {
    this.facade.create(library);
    this.libraryFormDialog.close();
  }
}
