import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { StyleClassModule } from 'primeng/styleclass';
import { ToastModule } from 'primeng/toast';
import { LibrarySidebarComponent } from './features/library/components/library-sidebar/library-sidebar.component';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { HealthService } from './core/services/health.service';
import { AppConfig } from './core/config/app-config.model';
import { APP_CONFIG } from './core/config/app-config.token';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    FormsModule,
    ButtonModule,
    SelectModule,
    ToastModule,
    StyleClassModule,
    LibrarySidebarComponent,
    TranslatePipe,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  theme: 'light' | 'dark' = 'light';
  isThemeSwitching = false;

  translate = inject(TranslateService);
  currentLanguage = 'de';
  languages = ['de', 'en'];

  health = inject(HealthService);
  config = inject<AppConfig>(APP_CONFIG);

  constructor() {
    // theme switching
    const darkModeOn =
      window.matchMedia &&
      window.matchMedia('(prefers-color-scheme: dark)').matches;

    this.theme = darkModeOn ? 'dark' : 'light';
    const element = document.querySelector('html');
    element!.classList.add(this.theme);

    // translation
    this.translate.addLangs(this.languages);
    this.translate.setDefaultLang(this.currentLanguage);
    const browserLang = this.translate.getBrowserLang();
    const lang = browserLang?.match(/en|de/)
      ? browserLang
      : this.currentLanguage;
    this.translate.use(lang);

    // health check
    this.health.startHealthMonitor(this.config.healthCheckInterval);
  }

  toggleTheme() {
    this.isThemeSwitching = true;

    const element = document.querySelector('html');
    element!.classList.remove(this.theme);

    this.theme = this.theme === 'light' ? 'dark' : 'light';
    element!.classList.add(this.theme);

    setTimeout(() => {
      this.isThemeSwitching = false;
    }, 300);
  }

  changeLanguage(lang: string) {
    this.translate.use(lang);
    this.currentLanguage = lang;
  }
}
