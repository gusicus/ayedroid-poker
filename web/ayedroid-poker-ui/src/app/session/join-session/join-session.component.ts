import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { WebApiService } from 'src/app/services/web-api.service';

@Component({
  selector: 'pkr-join-session',
  templateUrl: './join-session.component.html',
  styleUrls: ['./join-session.component.scss'],
})
export class JoinSessionComponent {
  constructor(
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService,
    private router: Router
  ) {}

  public joinSession(sessionId: string): void {
    this.webApiService.joinSession(sessionId).subscribe({
      next: (sessionId) => this.router.navigate(['/', sessionId]),
      error: (e: HttpErrorResponse) => {
        if (e.status === 404) {
          this.snackBar.open(
            this.translocoService.translate('MAIN.{{ENTITY}} DOES NOT EXIST', {
              ENTITY: sessionId,
            })
          );
          return;
        }

        this.snackBar
          .open(
            this.translocoService.translate('ERRORS.ERROR JOINING SESSION'),
            this.translocoService.translate('MAIN.RETRY')
          )
          .onAction()
          .subscribe(() => this.joinSession(sessionId));
      },
    });
  }
}
