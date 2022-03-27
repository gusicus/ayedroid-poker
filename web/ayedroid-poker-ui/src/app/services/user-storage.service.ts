import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { SessionDto, TokenDto } from '../models/web-api.model';

@Injectable({
  providedIn: 'root',
})
export class UserStorageService {
  private KEY_USERNAME = 'username';
  private KEY_TOKEN = 'token';
  private KEY_SIZES = 'sizes';

  public constructor() {}

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

  public activeSession$ = new ReplaySubject<SessionDto>(1);

  public set activeSession(session: SessionDto) {
    this.activeSession$.next(session);
  }

  public set sizes(sizes: string[]) {
    localStorage.setItem(this.KEY_SIZES, JSON.stringify(sizes));
  }

  public get sizes(): string[] {
    const sizes = localStorage.getItem(this.KEY_SIZES);
    return sizes ? JSON.parse(sizes) : null;
  }
}
