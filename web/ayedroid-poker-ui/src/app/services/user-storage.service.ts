import { Injectable } from '@angular/core';
import { TokenDto } from '../models/web-api.model';

@Injectable({
  providedIn: 'root',
})
export class UserStorageService {
  private KEY_USERNAME = 'username';
  private KEY_TOKEN = 'token';

  constructor() {}

  public set userName(userName: string) {
    localStorage.setItem(this.KEY_USERNAME, userName);
  }

  public get userName(): string {
    const userName = localStorage.getItem(this.KEY_USERNAME);
    return userName ? userName : '';
  }

  public set token(token: TokenDto) {
    localStorage.setItem(this.KEY_TOKEN, JSON.stringify(token));
  }

  public get token(): TokenDto {
    const token = localStorage.getItem(this.KEY_TOKEN);
    return token ? JSON.parse(token) : null;
  }
}
