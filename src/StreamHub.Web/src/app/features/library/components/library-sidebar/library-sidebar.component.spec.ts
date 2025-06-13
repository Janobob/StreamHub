import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { LibrarySidebarComponent } from './library-sidebar.component';

describe('LibrarySidebarComponent', () => {
  let component: LibrarySidebarComponent;
  let fixture: ComponentFixture<LibrarySidebarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [LibrarySidebarComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LibrarySidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
