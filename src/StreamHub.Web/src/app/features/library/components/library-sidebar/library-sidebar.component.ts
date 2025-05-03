import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { LibraryFacade } from '../../library.facade';
import { ButtonModule } from 'primeng/button';
import { SkeletonModule } from 'primeng/skeleton';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { BadgeModule } from 'primeng/badge';
import { ContextMenu, ContextMenuModule } from 'primeng/contextmenu';
import { LibraryFormDialogComponent } from '../dialogs/library-form-dialog/library-form-dialog.component';
import { Library } from '../../models/library.model';
import { MenuItem } from 'primeng/api';
import { LibraryDeleteDialogComponent } from '../dialogs/library-delete-dialog/library-delete-dialog.component';

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
    LibraryDeleteDialogComponent,
  ],
})
export class LibrarySidebarComponent implements OnInit {
  private readonly facade = inject(LibraryFacade);
  private readonly translate = inject(TranslateService);
  readonly libraries = this.facade.libraries;
  readonly loading = this.facade.loading;

  @ViewChild(ContextMenu)
  contextMenu!: ContextMenu;
  private selectedLibrary: Library | null = null;
  contextMenuItems: MenuItem[] | null = null;

  @ViewChild(LibraryFormDialogComponent)
  libraryFormDialog!: LibraryFormDialogComponent;

  @ViewChild(LibraryDeleteDialogComponent)
  libraryDeleteDialog!: LibraryDeleteDialogComponent;

  ngOnInit(): void {
    this.facade.loadAll();

    this.translate
      .get(['libraries.form.actions.edit', 'libraries.form.actions.delete'])
      .subscribe((translations) => {
        this.contextMenuItems = [
          {
            label: translations['libraries.form.actions.edit'],
            icon: 'pi pi-fw pi-pencil',
            command: () => {
              if (this.selectedLibrary) {
                this.onEditLibrary(this.selectedLibrary);
              }
            },
          },
          {
            label: translations['libraries.form.actions.delete'],
            icon: 'pi pi-fw pi-trash',
            command: () => {
              if (this.selectedLibrary) {
                this.onDeleteDialogLibrary(this.selectedLibrary);
              }
            },
          },
        ];
      });
  }

  onAddLibrary() {
    this.libraryFormDialog.open();
  }

  onEditLibrary(library: Library) {
    this.libraryFormDialog.open(library);
  }

  onDeleteDialogLibrary(library: Library) {
    this.libraryDeleteDialog.open(library);
  }

  onDeleteLibrary(library: Library) {
    this.facade.delete(library.id);
    this.libraryDeleteDialog.close();
  }

  onCreateLibrary(library: Library) {
    this.facade.create(library);
    this.libraryFormDialog.close();
  }

  onUpdateLibrary(library: Library) {
    this.facade.update(library);
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
