import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalR';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  public constructor() {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.baseUri}/Notifications`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  }
}
