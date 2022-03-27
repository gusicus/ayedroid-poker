import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSelectionListChange } from '@angular/material/list';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { ParticipantChange } from 'src/app/models/signal-r.models';
import { SessionDto } from 'src/app/models/web-api.model';
import { SignalRService } from 'src/app/services/signal-r.service';
import { UserStorageService } from 'src/app/services/user-storage.service';
import { WebApiService } from 'src/app/services/web-api.service';

@Component({
  selector: 'pkr-view-session',
  templateUrl: './view-session.component.html',
  styleUrls: ['./view-session.component.scss'],
})
export class ViewSessionComponent implements OnInit {
  public session: SessionDto | null = null;
  public sizes = ['S', 'M', 'L', 'XL'];
  public history: { name: string; description: string }[] = [];
  public currentTicket: { name: string; description: string } = {
    name: '',
    description: '',
  };
  public sizeChoice: string = '';

  public constructor(
    private activatedRoute: ActivatedRoute,
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService,
    private userStorageService: UserStorageService,
    private signalRService: SignalRService
  ) {}

  public ngOnInit(): void {
    for (let i = 0; i < 30; i++) {
      this.history.push({
        name: 'Ticket' + i,
        description: 'Description for ' + i,
      });
    }

    this.currentTicket =
      this.history[Math.floor(Math.random() * this.history.length + 0)];
    this.joinSession(this.activatedRoute.snapshot.params['sessionId']);

    this.signalRService.participantJoined$.subscribe(
      (newParticipant: ParticipantChange) => {
        if (this.session) {
          this.session.participants = [
            ...this.session.participants,
            newParticipant.participant,
          ];
        }
      }
    );
  }

  public joinSession(sessionId: string): void {
    this.webApiService.getSession(sessionId).subscribe({
      next: (session) => {
        this.session = session;
        this.userStorageService.activeSession = session;
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

  public onSizeChoiceChange(event: MatSelectionListChange): void {
    this.sizeChoice = event.source.selectedOptions.selected[0].value;
  }

  public nextTopic(): void {
    throw new Error('Method not implemented.');
  }

  public completeEarly(): void {
    throw new Error('Method not implemented.');
  }
}
