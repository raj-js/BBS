import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { QuestionsComponent } from './questions/questions.component';
import { ArticlesComponent } from './articles/articles.component';
import { QuestionComponent } from './question/question.component';
import { ArticleComponent } from './article/article.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotComponent } from './forgot/forgot.component';
import { ResetComponent } from './reset/reset.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { AddArticleComponent } from './add-article/add-article.component';
import { AuthGuard } from './@core/AuthGuard';
import { ActivateComponent } from './activate/activate.component';
import { PersonComponent } from './person/person.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'questions', component: QuestionsComponent },
  { path: 'question/:id', component: QuestionComponent },
  { path: 'addQuestion', component: AddQuestionComponent, canActivate: [AuthGuard] },
  { path: 'articles', component: ArticlesComponent },
  { path: 'article/:id', component: ArticleComponent },
  { path: 'addArticle', component: AddArticleComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'activate/:userId/:code', component: ActivateComponent },
  { path: 'forgot', component: ForgotComponent },
  { path: 'reset/:userId/:code', component: ResetComponent },
  { path: 'person/:id', component: PersonComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: '404', component: NotFoundComponent },
  { path: '', redirectTo: 'questions', pathMatch: 'full'},
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
