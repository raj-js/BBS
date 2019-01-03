import { Component, OnInit } from '@angular/core';
import { Apis } from '../@core/Apis';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService, CategoryService, ListItem2, ArticleType } from '../@core/ApiProxy';
import { User } from '../@core/User';
import { NbAuthJWTToken, NbAuthService } from '@nebular/auth';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {

  user: User = null;

  constructor(
    private msg: NzMessageService,
    private articleService: ArticleService,
    private categoryService: CategoryService,
    private authService: NbAuthService) {
    this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
      });
  }

  loading = true;
  noMoreArticle = false;
  articles: ListItem2[] = [];

  fiters = {
    title: undefined,
    keywords: undefined,
    categoryId: undefined,
    state: undefined,
    type: ArticleType.Article,
    orderBy: undefined,
    isAsc: true,
    pageIndex: 1,
    pageSize: Apis.PageSize
  };

  nodes: any = [];

  ngOnInit(): void {
    this.categoryService.all()
    .subscribe(resp => {
      if (resp.status === 200) {
        if (resp.result.success) {
          this.nodes = resp.result.body;
        }
      }
    });

    this.loadArticles();
  }

  loadArticles(): any {
    this.loading = true;
    this.articleService.search(this.fiters.title, this.fiters.keywords, this.fiters.categoryId, this.fiters.state,
      this.fiters.type, this.fiters.orderBy, this.fiters.isAsc, this.fiters.pageIndex, this.fiters.pageSize)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.fiters.pageIndex === 1) {
              this.articles = resp.result.body.dtos;
            } else {
              this.articles = this.articles.concat(resp.result.body.dtos);
            }
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.noMoreArticle = true;
            }
            this.fiters.pageIndex++;
          }
        } else {
          this.msg.error('加载数据异常');
        }
      });
  }

  onCategoryChange($event: string): void {
    this.fiters.pageIndex = 1;
    if (this.fiters.categoryId == null) {
      this.fiters.categoryId =  undefined;
    }

    this.loadArticles();
  }
}
