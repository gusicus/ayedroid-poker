import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import {
  ParticipantNotification,
  TopicNotification,
  TopicVoteNotification,
} from 'src/app/models/signal-r.models';
import {
  SessionDto,
  TopicDto,
  UniqueEntity,
} from 'src/app/models/web-api.model';
import { SignalRService } from 'src/app/services/signal-r.service';
import { UserStorageService } from 'src/app/services/user-storage.service';
import { WebApiService } from 'src/app/services/web-api.service';
import { TopicPromptComponent } from '../topic-prompt/topic-prompt.component';
import { TopicPromptResult } from '../topic-prompt/topic-prompt.model';

@Component({
  selector: 'pkr-view-session',
  templateUrl: './view-session.component.html',
  styleUrls: ['./view-session.component.scss'],
})
export class ViewSessionComponent implements OnInit {
  public session: SessionDto = {
    id: '',
    name: '',
    participants: [],
    sizes: [],
    topics: [],
  };

  public topicHistory: TopicDto[] = [];

  public currentTopic: TopicDto = {
    description: '',
    id: '',
    name: '',
    votes: {},
  };

  public sizeChoice: string = '';

  private selectionList: MatSelectionList | undefined = undefined;

  public constructor(
    private activatedRoute: ActivatedRoute,
    private webApiService: WebApiService,
    private snackBar: MatSnackBar,
    private translocoService: TranslocoService,
    private userStorageService: UserStorageService,
    private signalRService: SignalRService,
    private dialog: MatDialog
  ) {}

  public ngOnInit(): void {
    this.joinSession(this.activatedRoute.snapshot.params['sessionId']);

    this.signalRService.participantJoined$.subscribe(
      (newParticipant: ParticipantNotification) => {
        if (this.session) {
          this.session.participants = [
            ...this.session.participants,
            newParticipant.participant,
          ];
        }
      }
    );

    this.signalRService.newTopic$.subscribe(
      (topicNotification: TopicNotification) => {
        if (this.session) {
          if (this.currentTopic.id) {
            this.topicHistory = [this.currentTopic, ...this.topicHistory];
          }
          this.currentTopic = {
            description: topicNotification.topic.description,
            id: topicNotification.topic.id,
            name: topicNotification.topic.name,
            votes: {},
          };
          this.selectionList?.deselectAll();
          this.sizeChoice = '';
        }
      }
    );

    this.signalRService.newTopicVote$.subscribe(
      (topicNotification: TopicVoteNotification) => {
        if (this.session) {
          if (this.currentTopic.id === topicNotification.topicId) {
            const size = this.session.sizes.find(
              (s) => s.id === topicNotification.sizeId
            );

            if (!size) return;

            this.currentTopic.votes[topicNotification.userId] = size;
          }
        }
      }
    );
  }

  public joinSession(sessionId: string): void {
    this.webApiService.getSession(sessionId).subscribe({
      next: (session) => {
        this.session = session;
        this.userStorageService.activeSession = session;

        if (session.topics.length > 0) {
          this.currentTopic = {
            description: session.topics[0].description,
            id: session.topics[0].id,
            name: session.topics[0].name,
            votes: {},
          };
          this.topicHistory = session.topics.slice(1);
        }
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
    this.selectionList = event.source;
    this.castVote(this.session.id, this.currentTopic.id, this.sizeChoice);
  }

  private castVote(sessionId: string, topicId: string, sizeId: string): void {
    this.webApiService.castVote(sessionId, topicId, sizeId).subscribe({
      next: () => {
        // Let signalR handle it
      },
      error: (e: HttpErrorResponse) => {
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

  public nextTopic(): void {
    this.dialog
      .open(TopicPromptComponent)
      .afterClosed()
      .subscribe((result: TopicPromptResult) => {
        if (result) {
          this.startNewTopic(this.session.id, result.title, result.description);
        }
      });
  }

  private startNewTopic(
    sessionId: string,
    title: string,
    description: string
  ): void {
    this.webApiService.startNewTopic(sessionId, title, description).subscribe({
      next: () => {
        // Nothing. let signalR do the update for all users, including the initiator
      },
      error: (e: HttpErrorResponse) => {
        this.snackBar
          .open(
            this.translocoService.translate('ERRORS.ERROR JOINING SESSION'),
            this.translocoService.translate('MAIN.RETRY')
          )
          .onAction()
          .subscribe(() => this.startNewTopic(sessionId, title, description));
      },
    });
  }

  public completeEarly(): void {
    throw new Error('Method not implemented.');
  }
}
