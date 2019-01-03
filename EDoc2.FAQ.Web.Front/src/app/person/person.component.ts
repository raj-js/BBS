import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { ProfileResp, AccountService, ListItem, ListItem2, ArticleService, ArticleType, UserSimpleResp } from '../@core/ApiProxy';
import { NzMessageService, NzIconService } from 'ng-zorro-antd';
import { Apis } from '../@core/Apis';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { User } from '../@core/User';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss']
})
export class PersonComponent implements OnInit {
  id = '';

  user: User = null;
  showFollowState = false;
  followed = false;

  profile: ProfileResp = null;
  articles: ListItem2[] = [];
  questions: ListItem2[] = [];
  favorites: ListItem2[] = [];
  follows: UserSimpleResp[] = [];
  fans: UserSimpleResp[] = [];

  indexes = {
    article: 1,
    question: 1,
    favorite: 1,
    follow: 1,
    fan: 1
  };

  counter = {
    article: 0,
    question: 0,
    favorite: 0,
    follow: 0,
    fan: 0
  };

  loading = {
    article: true,
    question: true,
    favorite: true,
    follow: true,
    fan: true
  };

  complete = {
    article: false,
    question: false,
    favorite: false,
    follow: false,
    fan: false
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private msg: NzMessageService,
    private articleService: ArticleService,
    private authService: NbAuthService,
    private iconService: NzIconService) {
    this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
      });
    this.iconService.fetchFromIconfont({
      scriptUrl: Apis.IconScriptUrl
    });
  }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.id = params.get('id');
        return this.accountService.getProfile(this.id);
      })
    ).subscribe(resp => {
      if (resp.status === 200) {
        if (resp.result.success) {
          this.profile = resp.result.body;

          this.loadArticles();
          this.loadQuestions();
          this.loadFollows();
          this.loadFans();

          if (this.user.IsValid) {
            if (this.user.Id === this.id) {
              this.loadFavorite();
            } else {
              this.showFollowState = true;
              this.accountService.isFollow(this.id)
              .subscribe(r => {
                if (r.status === 200) {
                  this.followed = r.result.body;
                }
              });
            }
          }
        } else {
          this.router.navigate(['/404']);
        }
      } else {
        this.msg.error('网络异常');
      }
    });
  }

  //#region methods

  protected loadArticles(): void {
    this.loading.article = true;
    this.articleService.getUserArticles(this.id, ArticleType.Article,
      this.indexes.article, Apis.PageSize, undefined, undefined)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.indexes.article === 1) {
              this.articles = resp.result.body.dtos;
            } else {
              this.articles = this.articles.concat(resp.result.body.dtos);
            }
            this.counter.article = resp.result.body.totalCount;
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.complete.article = true;
            }
            this.indexes.article ++;
          }
        }
        this.loading.article = false;
      });
  }

  protected loadQuestions(): void {
    this.loading.question = true;
    this.articleService.getUserArticles(this.id, ArticleType.Question,
      this.indexes.question, Apis.PageSize, undefined, undefined)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.indexes.question === 1) {
              this.questions = resp.result.body.dtos;
            } else {
              this.questions = this.questions.concat(resp.result.body.dtos);
            }
            this.counter.question = resp.result.body.totalCount;
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.complete.question = true;
            }
            this.indexes.question ++;
          }
        }
        this.loading.question = false;
      });
  }

  protected loadFavorite(): void {
    this.loading.favorite = true;
    this.articleService.getUserFavorites(this.indexes.favorite, Apis.PageSize, undefined, undefined)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.indexes.favorite === 1) {
              this.favorites = resp.result.body.dtos;
            } else {
              this.favorites = this.favorites.concat(resp.result.body.dtos);
            }
            this.counter.favorite = resp.result.body.totalCount;
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.complete.favorite = true;
            }
            this.indexes.favorite ++;
          }
        }
        this.loading.favorite = false;
      });
  }

  loadFollows(): void {
    this.loading.follow = true;
    this.accountService.getFollows(this.indexes.favorite, Apis.PageSize, undefined, undefined, this.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.indexes.follow === 1) {
              this.follows = resp.result.body.dtos;
            } else {
              this.follows = this.follows.concat(resp.result.body.dtos);
            }
            this.counter.follow = resp.result.body.totalCount;
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.complete.follow = true;
            }
            this.indexes.follow ++;
          }
        }
        this.loading.follow = false;
      });
  }

  loadFans(): void {
    this.loading.fan = true;
    this.accountService.getFans(this.indexes.favorite, Apis.PageSize, undefined, undefined, this.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            if (this.indexes.fan === 1) {
              this.fans = resp.result.body.dtos;
            } else {
              this.fans = this.fans.concat(resp.result.body.dtos);
            }
            this.counter.fan = resp.result.body.totalCount;
            if (resp.result.body.dtos.length < Apis.PageSize) {
              this.complete.fan = true;
            }
            this.indexes.fan ++;
          }
        }
        this.loading.fan = false;
      });
  }

  //#endregion

  onFollow(): void {
    if (!this.user.IsValid) {
      this.msg.error('请先登录！');
      return;
    }

    this.accountService.followOrNot(this.id)
    .subscribe(resp => {
      if (resp.status === 200) {
        this.followed = !this.followed;
      }
    });
  }
}
