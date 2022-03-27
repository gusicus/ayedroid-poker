import { Injectable } from '@angular/core';
import { TranslocoService } from '@ngneat/transloco';
import { Observable } from 'rxjs';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root',
})
export class AppStartService {
  public constructor(
    private translocoService: TranslocoService,
    private signalRService: SignalRService
  ) {}

  public init(): Observable<unknown> {
    this.signalRService.startConnection();

    return this.translocoService.load('en');
  }
}
