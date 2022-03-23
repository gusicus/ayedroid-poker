import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { WebApiService } from '../services/web-api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public newSessionLoading: boolean = false;

  constructor(
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  public startSession(sessionName: string): void {
    this.newSessionLoading = true;

    this.webApiService
      .startSession(sessionName)
      .subscribe({
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
      })
      .add(() => (this.newSessionLoading = false));
  }
}
