import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { LibraryFormDialogComponent } from './library-form-dialog.component';

describe('LibraryFormDialogComponent', () => {
  let component: LibraryFormDialogComponent;
  let fixture: ComponentFixture<LibraryFormDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [LibraryFormDialogComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LibraryFormDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
