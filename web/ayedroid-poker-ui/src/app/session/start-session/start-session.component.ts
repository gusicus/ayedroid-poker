import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { UserStorageService } from 'src/app/services/user-storage.service';
import { WebApiService } from 'src/app/services/web-api.service';

@Component({
  selector: 'pkr-start-session',
  templateUrl: './start-session.component.html',
  styleUrls: ['./start-session.component.scss'],
})
export class StartSessionComponent {
  public defaultSizes: string[] = ['1', '3', '5', '8', '?'];
  public sizes: string[];

  public constructor(
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService,
    private router: Router,
    private userStorageService: UserStorageService
  ) {
    this.sizes = userStorageService.sizes
      ? userStorageService.sizes
      : this.defaultSizes;
  }

  public startSession(sessionName: string): void {
    this.webApiService.startSession(sessionName, this.sizes).subscribe({
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

  public removeSize(size: string): void {
    this.sizes = this.sizes.filter((s) => s !== size);
    this.userStorageService.sizes = this.sizes;
  }

  public addSizeFromInput(event: MatChipInputEvent): void {
    if (event.value) {
      this.sizes.push(event.value);
      event.chipInput!.clear();
      this.userStorageService.sizes = this.sizes;
    }
  }

  public chipDrop(event: CdkDragDrop<string[]>): void {
    moveItemInArray(this.sizes, event.previousIndex, event.currentIndex);
    this.userStorageService.sizes = this.sizes;
  }

  public resetSizes(): void {
    this.sizes = this.defaultSizes;
    this.userStorageService.sizes = this.sizes;
  }
}
