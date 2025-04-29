import {
  ApplicationConfig,
  provideZoneChangeDetection,
  inject,
  isDevMode,
} from '@angular/core';
import Aura from '@primeng/themes/aura';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { AppConfig } from './core/config/app-config.model';
import { APP_CONFIG } from './core/config/app-config.token';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { provideApollo } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { provideTranslateService, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { provideLibraryFeature } from './features/library/library.providers';
import { provideStoreDevtools } from '@ngrx/store-devtools';

const appRuntimeConfig: AppConfig = {
  useGraphQL: false,
};

const httpLoaderFactory: (http: HttpClient) => TranslateHttpLoader = (
  http: HttpClient
) => new TranslateHttpLoader(http, './i18n/', '.json');

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          darkModeSelector: '.dark',
        },
      },
    }),
    {
      provide: APP_CONFIG,
      useValue: appRuntimeConfig,
    },
    provideHttpClient(),
    provideApollo(() => {
      const httpLink = inject(HttpLink);
      return {
        link: httpLink.create({
          uri: '/graphql',
        }),
        cache: new InMemoryCache(),
      };
    }),
    provideStore(),
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
    provideEffects(),
    provideTranslateService({
      defaultLanguage: 'de',
      loader: {
        provide: TranslateLoader,
        useFactory: httpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    // features
    provideLibraryFeature(),
  ],
};
