import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgZorroAntdModule, NZ_I18N, zh_CN } from 'ng-zorro-antd';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import zh from '@angular/common/locales/zh';
import { LayoutsComponent } from './@theme/layouts/layouts.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { QuestionComponent } from './question/question.component';
import { ArticleComponent } from './article/article.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { AddArticleComponent } from './add-article/add-article.component';
import { QuestionsComponent } from './questions/questions.component';
import { ArticlesComponent } from './articles/articles.component';
import { RegisterComponent } from './register/register.component';
import { ForgotComponent } from './forgot/forgot.component';
import { ResetComponent } from './reset/reset.component';
import { LoginComponent } from './login/login.component';
import { QuestionStateComponent } from './@theme/question-state/question-state.component';
import { CategorySelectorComponent } from './@theme/category-selector/category-selector.component';
import { NbPasswordAuthStrategy, NbAuthModule, NbAuthJWTToken } from '@nebular/auth';
import { Apis } from './@core/Apis';
import { AuthGuard } from './@core/AuthGuard';
import { NbSecurityModule, NbRoleProvider } from '@nebular/security';
import { RoleProvider } from './@core/RoleProvider';
import { ActivateComponent } from './activate/activate.component';
import { FdyTimePipe } from './@theme/pipes/fdy-time.pipe';
import { PersonComponent } from './person/person.component';
import { FdyEmptyPipe } from './@theme/pipes/fdy-empty.pipe';
import { ProfileComponent } from './profile/profile.component';
import { HtmlPipe } from './@theme/pipes/html.pipe';
import { AvatarPipe } from './@theme/pipes/avatar.pipe';
import { BgImageDirective } from './@theme/directives/bg-image.directive';

registerLocaleData(zh);

@NgModule({
  declarations: [
    AppComponent,
    LayoutsComponent,
    NotFoundComponent,
    QuestionsComponent,
    QuestionComponent,
    ArticlesComponent,
    ArticleComponent,
    AddQuestionComponent,
    AddArticleComponent,
    LoginComponent,
    RegisterComponent,
    ForgotComponent,
    ResetComponent,
    QuestionStateComponent,
    CategorySelectorComponent,
    ActivateComponent,
    FdyTimePipe,
    PersonComponent,
    FdyEmptyPipe,
    ProfileComponent,
    HtmlPipe,
    AvatarPipe,
    BgImageDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgZorroAntdModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,

    NbAuthModule.forRoot({
      strategies: [
        NbPasswordAuthStrategy.setup({
          name: 'email',
          token: {
            class: NbAuthJWTToken,
            key: 'body',
          },
          baseEndpoint: Apis.BASE_URL,
          login: {
            endpoint: Apis.Authorize.Login,
            method: 'post',
          },
          logout: {
            endpoint: Apis.Authorize.Logout,
            method: 'post',
          }
        })
      ],
      forms: {},
    }),

    NbSecurityModule.forRoot({
      accessControl: {
        'GUEST': {
          view: ['question', 'article', 'comment', 'person'],
        },
        'MEMBER': {
          parent: 'GUEST',
          create: [ 'question', 'article', 'comment' ],
          edit: [ 'question', 'article', 'person' ],
          operate: [ 'like', 'follow', 'report', 'favorite' ]
        },
        'MODERATOR': {
          parent: 'MEMBER',
          mute: [ 'person', 'article', 'question', 'comment' ],
        },
        'ADMINISTRATOR': {
          parent: 'MODERATOR',
          mute: [ 'moderator' ],
        }
      },
    })
  ],
  providers: [
    { provide: NZ_I18N, useValue: zh_CN },
    AuthGuard,
    { provide: NbRoleProvider, useClass: RoleProvider },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
