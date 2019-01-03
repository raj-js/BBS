import { Component, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import {
  ArticleService,
  ArticleResp,
  CommentItem,
  ReplyArticleReq,
  FinishReq,
  ArticleState,
  AccountService,
  ArticleType
} from '../@core/ApiProxy';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { NzMessageService, NzIconService } from 'ng-zorro-antd';
import { Apis } from '../@core/Apis';
import * as wangEditor from 'wangeditor';
import { NbAccessChecker } from '@nebular/security';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { User } from '../@core/User';
import { EditorMenu } from '../@core/EditorConfig';
import { XssService } from '../@core/XssService';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent implements OnInit, AfterViewInit {
  id = '';
  question: ArticleResp = new ArticleResp();

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
    private xss: XssService,
    private iconService: NzIconService) {
    this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
      });
    this.replyReq.content = '';
    this.iconService.fetchFromIconfont({
      scriptUrl: Apis.IconScriptUrl
    });
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
          this.question = resp.result.body;
          if (this.question.typeId === ArticleType.Article) {
            this.router.navigate(['article', this.id]);
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

  onAdopt(comment: CommentItem): void {
    if (!this.user.IsValid) {
      this.msg.error('无效操作！');
      return;
    }

    const req = new FinishReq();
    req.id = this.id;
    req.adoptId = comment.id;
    req.unsatisfactory = false;

    this.articleService.finish(req)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            this.question = resp.result.body;
            this.topAdoptComment(comment);
            this.msg.success('结帖成功！');
          } else {
            if (resp.result.errors && resp.result.errors.length > 0) {
              let errorMsg = '';
              resp.result.errors.map((error, index, obj) => {
                errorMsg += '<br/>';
                errorMsg += error.description;
              });
              this.msg.error(`结帖失败: ${errorMsg}`);
              return;
            }
            this.msg.error('结帖失败,未知错误');
          }
        } else {
          this.msg.error('网络异常');
        }
      });
  }

  cancel(): void {
  }

  canFinish(commentCreatorId: string) {
    if (this.question.stateId !== ArticleState.UnSolved) {
      return false;
    }

    if (this.question.creatorId === commentCreatorId) {
      return false;
    }

    if (this.user.Id !== this.question.creatorId) {
      return false;
    }

    return true;
  }

  topAdoptComment(comment: CommentItem) {
    console.log('before sort', this.comments);
    this.comments = this.comments.sort((cur, next) => {
      if (cur.id === comment.id) {
        return -1;
      } else {
        return cur.creationTime < next.creationTime ? -1 : 1;
      }
    });
    console.log('after sort', this.comments);
  }
}
