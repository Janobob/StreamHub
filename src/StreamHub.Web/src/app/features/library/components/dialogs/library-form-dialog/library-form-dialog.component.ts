import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
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

@Component({
  standalone: true,
  selector: 'app-library-form-dialog',
  templateUrl: './library-form-dialog.component.html',
  styleUrls: ['./library-form-dialog.component.css'],
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

  private fb = inject(FormBuilder);

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
      this.close();
    } else {
      this.form.markAllAsTouched();
      this.form.updateValueAndValidity();
    }
  }
}
