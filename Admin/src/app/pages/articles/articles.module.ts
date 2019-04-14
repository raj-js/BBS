import { NgModule } from '@angular/core';
import { ArticlesComponent } from './articles.component';
import { ArticleListComponent } from './article-list/article-list.component';
import { ThemeModule } from '../../@theme/theme.module';
import { ArticlesRoutingModule } from './articles-routing.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { RendersModule } from '../../@theme/components/renders/renders.module';

const COMPONENTS = [
  ArticlesComponent,
  ArticleListComponent
];

@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    ThemeModule,
    ArticlesRoutingModule,
    Ng2SmartTableModule,
    RendersModule
  ]
})
export class ArticlesModule { }
