import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { WebApiService } from 'src/app/services/web-api.service';

@Component({
  selector: 'pkr-start-session',
  templateUrl: './start-session.component.html',
  styleUrls: ['./start-session.component.scss'],
})
export class StartSessionComponent {
  constructor(
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService,
    private router: Router
  ) {}

  public startSession(sessionName: string): void {
    this.webApiService.startSession(sessionName).subscribe({
      next: (sessionId) => this.router.navigate(['/', sessionId]),
      error: () => {
        this.snackBar
          .open(
            this.translocoService.translate('ERRORS.CREATE SESSION'),
            this.translocoService.translate('MAIN.RETRY')
          )
          .onAction()
          .subscribe(() => this.startSession(sessionName));
      },
    });
  }
}
