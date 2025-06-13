import {
  Component,
  EventEmitter,
  inject,
  Input,
  Output,
  effect,
} from '@angular/core';
import { Library } from '../../../models/library.model';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { TextareaModule } from 'primeng/textarea';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { LibraryFacade } from '../../../library.facade';
import { LoadingTypes } from '../../../store/library.reducer';

@Component({
  standalone: true,
  selector: 'app-library-form-dialog',
  templateUrl: './library-form-dialog.component.html',
  imports: [
    ButtonModule,
    DialogModule,
    InputTextModule,
    MessageModule,
    TextareaModule,
    ReactiveFormsModule,
    TranslateModule,
  ],
})
export class LibraryFormDialogComponent {
  @Input() library?: Library;
  @Output() createLibrary = new EventEmitter<Library>();
  @Output() updateLibrary = new EventEmitter<Library>();

  private readonly facade = inject(LibraryFacade);
  private readonly loadingType = this.facade.loadingType;

  private readonly fb = inject(FormBuilder);

  visible = false;
  form: FormGroup = this.fb.group({
    name: [
      this.library?.name ?? '',
      [Validators.required, Validators.maxLength(200)],
    ],
    description: [
      this.library?.description ?? '',
      [Validators.maxLength(1000)],
    ],
    path: [
      this.library?.path ?? '',
      [Validators.required, Validators.maxLength(1000)],
    ],
  });

  constructor() {
    effect(() => {
      if (this.facade.createdSuccess()) {
        this.close();
        this.facade.resetCreatedSuccess();
      }

      if (this.facade.updatedSuccess()) {
        this.close();
        this.facade.resetUpdatedSuccess();
      }
    });
  }

  public open(library?: Library) {
    this.visible = true;
    this.library = library;

    if (library) {
      this.form.patchValue(library);
    }
  }

  public close() {
    this.visible = false;
    this.form.reset();
  }

  get isLoading() {
    return (
      this.loadingType() === LoadingTypes.Create ||
      this.loadingType() === LoadingTypes.Update
    );
  }

  save() {
    if (this.form.valid) {
      const updatedLibrary: Library = {
        ...this.library,
        ...this.form.value,
      } as Library;

      if (this.library?.id) {
        this.updateLibrary.emit(updatedLibrary);
      } else {
        this.createLibrary.emit(updatedLibrary);
      }
    } else {
      this.form.markAllAsTouched();
      this.form.updateValueAndValidity();
    }
  }
}
