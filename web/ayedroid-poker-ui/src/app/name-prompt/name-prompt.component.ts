import { Component } from '@angular/core';
import { UserStorageService } from '../services/user-storage.service';

@Component({
  selector: 'pkr-name-prompt',
  templateUrl: './name-prompt.component.html',
  styleUrls: ['./name-prompt.component.scss'],
})
export class NamePromptComponent {
  public username: string = this.userStorageService.userName;

  public constructor(private userStorageService: UserStorageService) {}
}
