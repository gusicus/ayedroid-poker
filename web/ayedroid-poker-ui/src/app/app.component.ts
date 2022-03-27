import { Component } from '@angular/core';
import { UserStorageService } from './services/user-storage.service';

@Component({
  selector: 'pkr-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public constructor(public userStorageService: UserStorageService) {}
}
