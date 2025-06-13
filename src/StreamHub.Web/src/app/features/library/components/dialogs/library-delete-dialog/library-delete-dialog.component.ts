import {
  Component,
  effect,
  EventEmitter,
  inject,
  Input,
  Output,
} from '@angular/core';
import { Library } from '../../../models/library.model';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { TranslateModule } from '@ngx-translate/core';
import { LibraryFacade } from '../../../library.facade';
import { LoadingTypes } from '../../../store/library.reducer';

@Component({
  standalone: true,
  selector: 'app-library-delete-dialog',
  templateUrl: './library-delete-dialog.component.html',
  imports: [DialogModule, ButtonModule, TranslateModule],
})
export class LibraryDeleteDialogComponent {
  @Input() library?: Library;
  @Output() deleteLibrary = new EventEmitter<Library>();

  private readonly facade = inject(LibraryFacade);
  readonly loadingType = this.facade.loadingType;
  visible = false;

  constructor() {
    effect(() => {
      if (this.facade.deletedSuccess()) {
        this.close();
        this.facade.resetDeletedSuccess();
      }
    });
  }

  public open(library?: Library) {
    this.visible = true;
    this.library = library;
  }

  public close() {
    this.visible = false;
  }

  get isLoading() {
    return this.loadingType() === LoadingTypes.Delete;
  }

  delete() {
    if (this.library) {
      this.deleteLibrary.emit(this.library);
    }
  }
}
