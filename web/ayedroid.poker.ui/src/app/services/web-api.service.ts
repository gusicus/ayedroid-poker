import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WebApiService {
  private baseUri = 'https://localhost:7241';

  constructor(private http: HttpClient) {}

  public newUser(userName: string): Observable<string> {
    return this.http.post<string>(`${this.baseUri}/User`, {
      UserName: userName,
    });
  }

  public startSession(sessionName: string): Observable<string> {
    return this.http.post<string>(`${this.baseUri}/Session`, {
      SessionName: sessionName,
    });
  }
}
