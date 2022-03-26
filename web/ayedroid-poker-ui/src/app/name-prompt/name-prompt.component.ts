import { Component, OnInit } from '@angular/core';
import { UserStorageService } from '../services/user-storage.service';

@Component({
  selector: 'pkr-name-prompt',
  templateUrl: './name-prompt.component.html',
  styleUrls: ['./name-prompt.component.scss'],
})
export class NamePromptComponent implements OnInit {
  public username: string = this.userStorageService.userName;
  constructor(private userStorageService: UserStorageService) {}

  ngOnInit(): void {}
}
