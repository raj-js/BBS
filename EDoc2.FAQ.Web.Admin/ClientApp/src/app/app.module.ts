/**
 * @license
 * Copyright Akveo. All Rights Reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 */
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from './@core/core.module';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ThemeModule } from './@theme/theme.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NbAuthModule, NbPasswordAuthStrategy, NbAuthJWTToken } from '@nebular/auth';
import { Apis } from './@core/data/Config';
import { AuthGuard } from './auth-guard.service';
import { NbSecurityModule, NbRoleProvider } from '@nebular/security';
import { RoleProvider } from './@core/auth/role.provider';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,

    NgbModule.forRoot(),
    ThemeModule.forRoot(),
    CoreModule.forRoot(),
    
    NbAuthModule.forRoot({
      strategies: [
        NbPasswordAuthStrategy.setup({
          name: "email",
          token: {
            class: NbAuthJWTToken,
            key: "body",
          },
          baseEndpoint: Apis.Endpoint,
          login: {
            endpoint: Apis.Authorize,
            method: "post",
          },
          logout: {
            endpoint: Apis.Logout,
            method: "post"
          },
          requestPass: {
            endpoint: Apis.ForgetPass,
            method: "post"
          },
          resetPass: {
            endpoint: Apis.ResetPass,
            method: "post"
          }
        })
      ],
      forms: {
        login: {
          redirectDelay: 100,
          strategy: 'email',
          showMessages: {     
            success: true,
            error: true,
          },
        },
        logout: {
          redirectDelay: 100,
          strategy: 'email',
        },
        requestPass:{
          redirectDelay: 100,
          strategy: 'email',
          showMessages: {     
            success: true,
            error: true,
          },
        },
        resetPass:{
          redirectDelay: 100,
          strategy: 'email',
          showMessages: {     
            success: true,
            error: true,
          },
        },
        validation: {
          password: {
            required: true,
            minLength: 6,
            maxLength: 50,
          },
          email: {
            required: true,
          }
        },
      }
    }),

    NbSecurityModule.forRoot({
      accessControl: {
        'MODERATOR': {
          view: ["user"]
        },
        'ADMINISTRATOR': {
          parent: "MODERATOR"
        },
      }
    })
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: APP_BASE_HREF, useValue: '/' },
    AuthGuard,
    { provide: NbRoleProvider, useClass: RoleProvider },
  ]
})
export class AppModule {
}
