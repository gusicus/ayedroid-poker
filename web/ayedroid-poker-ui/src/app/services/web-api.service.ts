import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { concatMap, EMPTY, map, Observable, of, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SessionDto, TokenDto, UniqueEntity } from '../models/web-api.model';
import { NamePromptComponent } from '../name-prompt/name-prompt.component';
import { UserStorageService } from './user-storage.service';

@Injectable({
  providedIn: 'root',
})
export class WebApiService {
  private baseUri = environment.baseApiUri;

  public constructor(
    private http: HttpClient,
    private userStorageService: UserStorageService,
    private dialog: MatDialog
  ) {}

  public login(userName: string): Observable<TokenDto> {
    return this.http.post<TokenDto>(`${this.baseUri}/Token`, {
      UserName: userName,
    });
  }

  public refreshToken(refreshToken: string): Observable<TokenDto> {
    return this.http.post<TokenDto>(`${this.baseUri}/Token/Refresh`, {
      RefreshToken: refreshToken,
    });
  }

  public startSession(
    sessionName: string,
    sizes: string[]
  ): Observable<string> {
    return this.userNameCheck().pipe(
      switchMap(() =>
        this.http.post<string>(`${this.baseUri}/Session`, {
          SessionName: sessionName,
          Sizes: sizes,
        })
      )
    );
  }

  public joinSession(sessionId: string): Observable<unknown> {
    return this.userNameCheck().pipe(
      switchMap(() =>
        this.http.post(`${this.baseUri}/Session/${sessionId}/Join`, null)
      )
    );
  }

  private userNameCheck(): Observable<string> {
    return this.dialog
      .open(NamePromptComponent)
      .afterClosed()
      .pipe(
        concatMap((username) => {
          if (!username) return EMPTY;
          this.userStorageService.userName = username;
          return of(username);
        })
      );
  }

  public getSession(sessionId: string): Observable<SessionDto> {
    return this.http
      .get<SessionDto>(`${this.baseUri}/Session/${sessionId}`)
      .pipe(
        map((sessionDto) => {
          sessionDto.topics.forEach((t) =>
            t.votes ? t.votes : new Map<string, UniqueEntity>()
          );

          return sessionDto;
        })
      );
  }

  public startNewTopic(
    sessionId: string,
    title: string,
    description: string
  ): Observable<string> {
    return this.http.post<string>(
      `${this.baseUri}/Session/${sessionId}/Topic`,
      {
        Title: title,
        Description: description,
      }
    );
  }

  public castVote(
    sessionId: string,
    topicId: string,
    sizeId: string
  ): Observable<unknown> {
    return this.http.put<unknown>(
      `${this.baseUri}/Session/${sessionId}/${topicId}/Vote?sizeId=${sizeId}`,
      {}
    );
  }
}
