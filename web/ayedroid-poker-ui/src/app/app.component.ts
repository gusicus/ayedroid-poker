import { Component } from '@angular/core';
import { UserStorageService } from './services/user-storage.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'pkr-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public constructor(
    public userStorageService: UserStorageService,
    public clipboard: Clipboard,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService
  ) {}

  public copyUrl(): void {
    this.copyToClipboard(window.location.href);
  }

  private copyToClipboard(str: string): void {
    const pending = this.clipboard.beginCopy(str);

    let remainingAttempts = 3;
    const attempt = (): void => {
      const result = pending.copy();
      if (!result && --remainingAttempts) {
        setTimeout(attempt);
      } else {
        // Remember to destroy when you're done!
        this.snackBar.open(
          this.translocoService.translate(
            'MAIN.{{ENTITY}} COPIED TO CLIPBOARD',
            {
              ENTITY: str,
            }
          )
        );
        pending.destroy();
      }
    };
    attempt();
  }
}
