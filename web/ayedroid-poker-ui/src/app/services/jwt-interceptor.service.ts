import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, filter, finalize, switchMap, take } from 'rxjs/operators';
import { UserStorageService } from './user-storage.service';
import { WebApiService } from './web-api.service';

@Injectable({
  providedIn: 'root',
})
export class JwtInterceptorService implements HttpInterceptor {
  private refreshTokenInProgress = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  constructor(
    private webApiService: WebApiService,
    private userStorageService: UserStorageService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (!request.url.includes('api/')) return next.handle(request);

    return next.handle(this.addAuthToken(request)).pipe(
      catchError((requestError: HttpErrorResponse) => {
        if (requestError && requestError.status === 401) {
          if (
            !this.userStorageService.token ||
            requestError.headers.has('invalid-token')
          ) {
            return this.webApiService
              .login(this.userStorageService.userName)
              .pipe(
                switchMap((token) => {
                  this.userStorageService.token = token;
                  return next.handle(this.addAuthToken(request));
                })
              );
          }

          if (requestError.headers.has('token-expired')) {
            if (this.refreshTokenInProgress) {
              return this.refreshTokenSubject.pipe(
                filter((result) => !!result),
                take(1),
                switchMap(() => next.handle(this.addAuthToken(request)))
              );
            } else {
              this.refreshTokenInProgress = true;
              this.refreshTokenSubject.next(null);

              return this.webApiService
                .refreshToken(this.userStorageService.token.refreshToken)
                .pipe(
                  switchMap((token) => {
                    this.userStorageService.token = token;
                    return next.handle(this.addAuthToken(request));
                  }),
                  finalize(() => (this.refreshTokenInProgress = false))
                );
            }
          }
        }

        throw requestError;
      })
    );
  }

  addAuthToken(request: HttpRequest<any>) {
    const token = this.userStorageService.token?.token;
    if (!token) return request;

    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
