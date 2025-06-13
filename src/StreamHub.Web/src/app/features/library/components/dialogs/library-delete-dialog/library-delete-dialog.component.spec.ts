import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { LibraryDeleteDialogComponent } from './library-delete-dialog.component';

describe('LibraryDeleteDialogComponent', () => {
  let component: LibraryDeleteDialogComponent;
  let fixture: ComponentFixture<LibraryDeleteDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [LibraryDeleteDialogComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LibraryDeleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
