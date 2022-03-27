import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalR';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ParticipantChange } from '../models/signal-r.models';
import { ParticipantDto } from '../models/web-api.model';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  public participantJoined$ = new ReplaySubject<ParticipantChange>(1);
  public participantLeft$ = new ReplaySubject<ParticipantChange>(1);

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
  }
}
