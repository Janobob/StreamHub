import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { StyleClassModule } from 'primeng/styleclass';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ButtonModule, StyleClassModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  theme: 'light' | 'dark' = 'light';

  constructor() {
    const darkModeOn =
      window.matchMedia &&
      window.matchMedia('(prefers-color-scheme: dark)').matches;

    this.theme = darkModeOn ? 'dark' : 'light';
    const element = document.querySelector('html');
    element!.classList.add(this.theme);
  }

  toggleTheme() {
    const element = document.querySelector('html');
    element!.classList.remove(this.theme);
    this.theme = this.theme === 'light' ? 'dark' : 'light';
    element!.classList.toggle(this.theme);
  }
}
