import { Component, inject, ViewChild } from '@angular/core';
import { LibraryFacade } from '../../library.facade';
import { ButtonModule } from 'primeng/button';
import { SkeletonModule } from 'primeng/skeleton';
import { TranslateModule } from '@ngx-translate/core';
import { BadgeModule } from 'primeng/badge';
import { ContextMenu, ContextMenuModule } from 'primeng/contextmenu';
import { LibraryFormDialogComponent } from '../dialogs/library-form-dialog/library-form-dialog.component';
import { Library } from '../../models/library.model';
import { MenuItem } from 'primeng/api';

@Component({
  standalone: true,
  selector: 'app-library-sidebar',
  templateUrl: './library-sidebar.component.html',
  styleUrls: ['./library-sidebar.component.css'],
  imports: [
    ButtonModule,
    SkeletonModule,
    BadgeModule,
    ContextMenuModule,
    TranslateModule,
    LibraryFormDialogComponent,
  ],
})
export class LibrarySidebarComponent {
  private readonly facade = inject(LibraryFacade);
  readonly libraries = this.facade.libraries;
  readonly loading = this.facade.loading;

  @ViewChild(ContextMenu)
  contextMenu!: ContextMenu;
  private selectedLibrary: Library | null = null;
  contextMenuItems: MenuItem[] = [
    {
      label: 'Bearbeiten',
      icon: 'pi pi-fw pi-pencil',
      command: () => {
        if (this.selectedLibrary) {
          this.onEditLibrary(this.selectedLibrary);
        }
      },
    },
    {
      label: 'Löschen',
      icon: 'pi pi-fw pi-trash',
      command: () => {
        if (this.selectedLibrary) {
          this.onDeleteLibrary(this.selectedLibrary);
        }
      },
    },
  ];

  @ViewChild(LibraryFormDialogComponent)
  libraryFormDialog!: LibraryFormDialogComponent;

  constructor() {
    this.facade.loadAll();
  }

  onAddLibrary() {
    this.libraryFormDialog.open();
  }

  onEditLibrary(library: Library) {
    this.libraryFormDialog.open(library);
  }

  onDeleteLibrary(library: Library) {}

  onSaveLibrary(library: Library) {
    this.facade.create(library);
    this.libraryFormDialog.close();
  }

  onContextMenuOpen(event: MouseEvent, library: Library) {
    this.selectedLibrary = library;
    this.contextMenu.show(event);
  }

  onContextMenuHide() {
    this.selectedLibrary = null;
  }
}
