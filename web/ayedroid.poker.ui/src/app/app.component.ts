import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslocoService } from '@ngneat/transloco';
import { WebApiService } from './services/web-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public newSessionLoading: boolean = false;
  constructor(
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService
  ) {}

  public startSession(): void {
    this.newSessionLoading = true;

    this.webApiService
      .startSession('test')
      .subscribe({
        error: () =>
          this.snackBar
            .open(
              this.translocoService.translate('ERRORS.CREATE SESSION'),
              this.translocoService.translate('MAIN.RETRY')
            )
            .onAction()
            .subscribe(() => this.startSession()),
        complete: () => console.info('complete'),
      })
      .add(() => (this.newSessionLoading = false));
  }
}
