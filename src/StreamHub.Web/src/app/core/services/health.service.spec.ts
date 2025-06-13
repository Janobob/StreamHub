import { TestBed, inject } from '@angular/core/testing';
import { HealthService } from './health.service';
import { provideTranslateService } from '@ngx-translate/core';
import { MessageService } from 'primeng/api';
import { provideHttpClient } from '@angular/common/http';

describe('Service: Health', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        HealthService,
        provideHttpClient(),
        provideTranslateService(),
        MessageService,
      ],
    });
  });

  it('should be created', inject([HealthService], (service: HealthService) => {
    expect(service).toBeTruthy();
  }));
});
