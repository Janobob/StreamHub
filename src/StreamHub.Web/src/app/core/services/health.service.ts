import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MessageService } from 'primeng/api';
import { map, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HealthService {
  private backendIsAvailable = true;
  private intervalId?: ReturnType<typeof setInterval>;
  private readonly http = inject(HttpClient);
  private readonly messageService = inject(MessageService);
  private readonly translate = inject(TranslateService);

  /**
   * Checks if the backend is available by calling the /health endpoint.
   * Returns an observable that emits true (healthy) or false (unreachable).
   */
  checkHealth() {
    return this.http.get('/health', { responseType: 'text' }).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

  /**
   * Starts a health check interval that monitors backend availability.
   */
  startHealthMonitor(intervalMs = 60_000) {
    this.checkHealth().subscribe((isAvailable) =>
      this.handleStatusChange(isAvailable)
    );

    this.intervalId = setInterval(() => {
      this.checkHealth().subscribe((isAvailable) =>
        this.handleStatusChange(isAvailable)
      );
    }, intervalMs);
  }

  /**
   * Stop health check polling.
   */
  stopHealthMonitor() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  /**
   * Handles state transitions and shows appropriate toast.
   */
  private handleStatusChange(isAvailable: boolean) {
    if (isAvailable && !this.backendIsAvailable) {
      this.messageService.add({
        severity: 'success',
        summary: this.translate.instant('app.messages.backendAvailable.title'),
        detail: this.translate.instant('app.messages.backendAvailable.detail'),
        life: 4000,
      });
    }

    if (!isAvailable && this.backendIsAvailable) {
      this.messageService.add({
        severity: 'warn',
        summary: this.translate.instant(
          'app.messages.backendUnavailable.title'
        ),
        detail: this.translate.instant(
          'app.messages.backendUnavailable.detail'
        ),
        life: 5000,
      });
    }

    this.backendIsAvailable = isAvailable;
  }
}
