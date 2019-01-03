import { Component, OnInit, Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService, ArticleType, CategoryService, ListItem2 } from '../@core/ApiProxy';
import { Apis } from '../@core/Apis';
import { User } from '../@core/User';
import { NbAuthJWTToken, NbAuthService } from '@nebular/auth';

const QuestionType: ArticleType = ArticleType.Question;

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.scss']
})
@Injectable({
  providedIn: 'root'
})
export class QuestionsComponent implements OnInit {

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
  noMoreQuestion = false;
  questions: ListItem2[] = [];

  fiters = {
    title: undefined,
    keywords: undefined,
    categoryId: undefined,
    state: undefined,
    type: QuestionType,
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

    this.loadQuestions();
  }

  loadQuestions(): any {
    this.loading = true;
    this.articleService.search(this.fiters.title, this.fiters.keywords, this.fiters.categoryId, this.fiters.state,
      this.fiters.type, this.fiters.orderBy, this.fiters.isAsc, this.fiters.pageIndex, this.fiters.pageSize)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.fiters.pageIndex === 1) {
              this.questions = resp.result.body.dtos;
            } else {
              this.questions = this.questions.concat(resp.result.body.dtos);
            }
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.noMoreQuestion = true;
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

    this.loadQuestions();
  }

  onStateChange($event): void {
    // event 之后才会更新值
    this.fiters.pageIndex = 1;
    this.fiters.state = $event;

    this.loadQuestions();
  }
}
