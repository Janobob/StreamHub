import { Component, inject } from '@angular/core';
import { LibraryFacade } from '../../library.facade';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-library-sidebar',
  templateUrl: './library-sidebar.component.html',
  styleUrls: ['./library-sidebar.component.css'],
  imports: [ButtonModule],
  standalone: true,
})
export class LibrarySidebarComponent {
  private readonly facade = inject(LibraryFacade);
  readonly libraries = this.facade.libraries;
  readonly loading = this.facade.loading;

  constructor() {
    this.facade.loadAll();
  }
}
