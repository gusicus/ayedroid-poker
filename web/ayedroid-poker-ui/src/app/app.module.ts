import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { WebApiService } from './services/web-api.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslocoRootModule } from '../transloco/transloco-root.module';
import { MatCardModule } from '@angular/material/card';
import { HomeComponent } from './home/home.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { StartSessionComponent } from './session/start-session/start-session.component';
import { JwtInterceptorService } from './services/jwt-interceptor.service';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { NamePromptComponent } from './name-prompt/name-prompt.component';
import { JoinSessionComponent } from './session/join-session/join-session.component';
import { ViewSessionComponent } from './session/view-session/view-session.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    StartSessionComponent,
    NamePromptComponent,
    JoinSessionComponent,
    ViewSessionComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    HttpClientModule,
    MatInputModule,
    MatTooltipModule,
    FlexLayoutModule,
    TranslocoRootModule,
    MatCardModule,
    MatSnackBarModule,
    FormsModule,
    MatDialogModule,
    MatListModule,
  ],
  providers: [
    WebApiService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
