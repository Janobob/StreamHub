import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { APP_CONFIG } from './core/config/app-config.token';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideTranslateService } from '@ngx-translate/core';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: {
            healthCheckInterval: 60000, // 60 Sekunden
          },
        },
        provideHttpClientTesting(),
        provideTranslateService(),
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
