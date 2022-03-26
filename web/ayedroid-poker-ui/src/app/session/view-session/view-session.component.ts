import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Session } from 'src/app/models/web-api.model';
import { WebApiService } from 'src/app/services/web-api.service';

@Component({
  selector: 'pkr-view-session',
  templateUrl: './view-session.component.html',
  styleUrls: ['./view-session.component.scss'],
})
export class ViewSessionComponent implements OnInit {
  public session: Session | null = null;

  public constructor(
    private activatedRoute: ActivatedRoute,
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService
  ) {}

  public ngOnInit(): void {
    this.joinSession(this.activatedRoute.snapshot.params['sessionId']);
  }

  public joinSession(sessionId: string): void {
    this.webApiService.getSession(sessionId).subscribe({
      next: (session) => {
        this.session = session;
      },
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
