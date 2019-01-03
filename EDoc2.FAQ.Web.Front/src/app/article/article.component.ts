import { Component, OnInit, ElementRef, AfterViewInit } from '@angular/core';
import { CommentItem, ArticleService, AccountService, ReplyArticleReq, ArticleResp, ArticleType } from '../@core/ApiProxy';
import { Apis } from '../@core/Apis';
import * as wangEditor from 'wangeditor';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { NbAccessChecker } from '@nebular/security';
import { User } from '../@core/User';
import { switchMap } from 'rxjs/operators';
import { XssService } from '../@core/XssService';
import { EditorMenu } from '../@core/EditorConfig';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit, AfterViewInit {
  id = '';
  article: ArticleResp = new ArticleResp();

  commentPageIndex = 1;
  loadingMore = false;
  comments: CommentItem[] = [];

  private editor: any;
  replyReq: ReplyArticleReq = new ReplyArticleReq();

  user: User = null;
  favorite = false;

  constructor(
    private el: ElementRef,
    private route: ActivatedRoute,
    private router: Router,
    private msg: NzMessageService,
    private articleService: ArticleService,
    private authService: NbAuthService,
    public accessChecker: NbAccessChecker,
    private accountService: AccountService,
    private xss: XssService) {
      this.article.canComment = true;

      this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
      });

      this.replyReq.content = '';
    }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.id = params.get('id');
        this.replyReq.articleId = this.id;
        this.replyReq.content = '';
        return this.articleService.view(this.id);
      })
    ).subscribe(resp => {
      if (resp.status === 200) {
        if (resp.result.success) {
          this.article = resp.result.body;
          if (this.article.typeId === ArticleType.Question) {
            this.router.navigate(['question', this.id]);
          }
          this.loadComments(this.id, (dtos) => {
            this.comments = dtos;
          });
        } else {
          this.router.navigate(['/404']);
        }
      } else {
        this.msg.error('网络异常');
      }
    });
  }

  ngAfterViewInit(): void {
    if (this.article.canComment) {
      const elToolbar = this.el.nativeElement.querySelector('#toolbar');
      const elEditor = this.el.nativeElement.querySelector('#editor');

      if (elToolbar && elEditor) {
        this.editor = new wangEditor(elToolbar, elEditor);

        const self = this;
        this.editor.customConfig.onchange = function (html) {
          self.replyReq.content = self.xss.process(html);
        };
        this.editor.customConfig.menus = EditorMenu;
        this.editor.create();
      }
    }

    if (this.user.IsValid) {
      this.accountService.isFavorite(this.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          this.favorite = resp.result.body;
        }
      });
    }
  }

  onLoadMore(): void {
    this.loadComments(this.id, resp => {
      this.comments = this.comments.concat(resp);
    });
  }

  loadComments(id: string, callback): void {
    this.loadingMore = true;
    this.articleService.getComments(id, this.commentPageIndex, Apis.CommentPageSize)
      .subscribe(resp => {
        this.loadingMore = false;
        if (resp.status === 200) {
          if (resp.result.success) {
            if (resp.result.body.dtos.length === 0) {
              this.loadingMore = true;
              return;
            }
            this.commentPageIndex++;
            callback(resp.result.body.dtos);

            if (resp.result.body.dtos.length < Apis.CommentPageSize) {
              this.loadingMore = true;
            }
          } else {
            this.msg.error('加载评论异常: ' + resp.result.errors);
          }
        } else {
          this.msg.error('网络异常');
        }
      });
  }

  onReply(): void {
    if (!this.article.canComment) {
      this.msg.error('不允许评论！');
      return;
    }

    this.authService.isAuthenticated()
      .subscribe(isAuthenticate => {
        if (!isAuthenticate) {
          this.msg.error('请先登录！');
          return;
        }

        if (this.replyReq.content.trim().length === 0) {
          this.msg.error('回复内容不能为空');
          return;
        }

        this.articleService.reply(this.replyReq)
          .subscribe(resp => {
            if (resp.status === 200) {
              if (resp.result.success) {
                this.comments = this.comments.concat(resp.result.body);
                this.editor.txt.html('');
              } else {
                this.msg.error('回复失败: ' + resp.result.errors);
              }
            } else {
              this.msg.error('网络异常');
            }
          });
      });
  }

  onFavorite(): void {
    if (!this.user.IsValid) {
      this.msg.error('请先登录！');
      return;
    }

    this.accountService.favoriteOrNot(this.id)
    .subscribe(resp => {
      if (resp.status === 200) {
        this.favorite = !this.favorite;
      }
    });
  }

  onLikeComment(comment: CommentItem): void {
    this.articleService.likeComment(comment.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            comment.likes = resp.result.body.likes;
            comment.dislikes = resp.result.body.dislikes;
          }
        } else {
          this.msg.error('网络异常');
        }
      });
  }

  onDislikeComment(comment: CommentItem): void {
    this.articleService.disLikeComment(comment.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            comment.likes = resp.result.body.likes;
            comment.dislikes = resp.result.body.dislikes;
          }
        } else {
          this.msg.error('网络异常');
        }
      });
  }

  onLike(): void {
    this.articleService.like(this.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            this.article.likes = resp.result.body.likes;
            this.article.dislikes = resp.result.body.dislikes;
          }
        } else {
          this.msg.error('网络异常');
        }
      });
  }

  onDislike(): void {
    this.articleService.dislike(this.id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            this.article.likes = resp.result.body.likes;
            this.article.dislikes = resp.result.body.dislikes;
          }
        } else {
          this.msg.error('网络异常');
        }
      });
  }

}
