import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalR';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import {
  ParticipantNotification,
  TopicNotification,
  TopicVoteNotification,
} from '../models/signal-r.models';
import { ParticipantDto, TopicDto } from '../models/web-api.model';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  public participantJoined$ = new ReplaySubject<ParticipantNotification>(1);
  public participantLeft$ = new ReplaySubject<ParticipantNotification>(1);
  public newTopic$ = new ReplaySubject<TopicNotification>(1);
  public newTopicVote$ = new ReplaySubject<TopicVoteNotification>(1);

  public constructor() {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(`${environment.baseUri}/Notifications`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));

    this.hubConnection.on(
      'ParticipantJoined',
      (sessionId: string, participant: ParticipantDto): void => {
        this.participantJoined$.next({ sessionId, participant });
      }
    );

    this.hubConnection.on(
      'ParticipantLeft',
      (sessionId: string, participant: ParticipantDto): void => {
        this.participantLeft$.next({ sessionId, participant });
      }
    );

    this.hubConnection.on(
      'NewTopic',
      (sessionId: string, topic: TopicDto): void => {
        this.newTopic$.next({ sessionId, topic });
      }
    );

    this.hubConnection.on(
      'NewTopicVote',
      (
        sessionId: string,
        topicId: string,
        userId: string,
        sizeId: string
      ): void => {
        this.newTopicVote$.next({ sessionId, topicId, userId, sizeId });
      }
    );
  }
}
