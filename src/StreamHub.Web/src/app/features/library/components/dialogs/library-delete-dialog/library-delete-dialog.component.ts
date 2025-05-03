import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Library } from '../../../models/library.model';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  standalone: true,
  selector: 'app-library-delete-dialog',
  templateUrl: './library-delete-dialog.component.html',
  styleUrls: ['./library-delete-dialog.component.css'],
  imports: [DialogModule, ButtonModule, TranslateModule],
})
export class LibraryDeleteDialogComponent {
  @Input() library?: Library;
  @Output() deleteLibrary = new EventEmitter<Library>();

  visible = false;

  public open(library?: Library) {
    this.visible = true;
    this.library = library;
  }

  public close() {
    this.visible = false;
  }

  delete() {
    if (this.library) {
      this.deleteLibrary.emit(this.library);
    }
    this.close();
  }
}
