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
import { InMemoryCache, split } from '@apollo/client/core';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { provideTranslateService, TranslateLoader } from '@ngx-translate/core';
import {
  provideTranslateHttpLoader,
  TranslateHttpLoader,
} from '@ngx-translate/http-loader';
import { provideLibraryFeature } from './features/library/library.providers';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { MessageService } from 'primeng/api';
import { createClient } from 'graphql-ws';
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { getMainDefinition } from '@apollo/client/utilities';
import { Kind, OperationTypeNode } from 'graphql';

const appRuntimeConfig: AppConfig = {
  useGraphQL: true,
  healthCheckInterval: 60000, // 1 minute
};

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
    MessageService,
    {
      provide: APP_CONFIG,
      useValue: appRuntimeConfig,
    },
    provideHttpClient(),
    provideApollo(() => {
      const httpLink = inject(HttpLink);
      const ws = new GraphQLWsLink(
        createClient({
          url:
            (window.location.protocol === 'https:' ? 'wss://' : 'ws://') +
            window.location.host +
            '/graphql',
        })
      );
      const http = httpLink.create({
        uri: '/graphql',
      });

      const link = split(
        ({ query }) => {
          const definition = getMainDefinition(query);
          return (
            definition.kind === Kind.OPERATION_DEFINITION &&
            definition.operation === OperationTypeNode.SUBSCRIPTION
          );
        },
        ws,
        http
      );

      return {
        link,
        cache: new InMemoryCache(),
      };
    }),
    provideStore(),
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
    provideEffects(),
    provideTranslateService({
      fallbackLang: 'de',
      loader: provideTranslateHttpLoader({
        prefix: './i18n/',
        suffix: '.json',
      }),
    }),
    // features
    provideLibraryFeature(),
  ],
};
