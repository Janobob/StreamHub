import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LibrarySidebarComponent } from './library-sidebar.component';
import { provideMockStore } from '@ngrx/store/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('LibrarySidebarComponent', () => {
  let component: LibrarySidebarComponent;
  let fixture: ComponentFixture<LibrarySidebarComponent>;

  TestBed.configureTestingModule({
    imports: [LibrarySidebarComponent],
    providers: [provideMockStore(), provideHttpClientTesting()],
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LibrarySidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
