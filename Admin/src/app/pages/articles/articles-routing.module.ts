import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArticlesComponent } from './articles.component';
import { ArticleListComponent } from './article-list/article-list.component';

const routes: Routes = [
  {
    path: '',
    component: ArticlesComponent,
    children: [
      {
        path: 'list',
        component: ArticleListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticlesRoutingModule { }
