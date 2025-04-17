import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'libraries',
    loadChildren: () =>
      import('./features/library/library.module').then((m) => m.LibraryModule),
  },
];
