import { Component, ElementRef, Output, EventEmitter, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import * as wangEditor from 'wangeditor';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { CategoryService, ArticleService, AddArticleReq } from '../@core/ApiProxy.js';
import * as xss from 'xss';
import { NzMessageService } from 'ng-zorro-antd';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { User } from '../@core/User.js';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.scss']
})
export class AddArticleComponent implements OnInit, AfterViewInit {

  private editor: any;
  @Output() PostData = new EventEmitter();

  validateForm: FormGroup;
  categories = [];

  user: User = null;

  constructor(private el: ElementRef,
    private fb: FormBuilder,
    private msg: NzMessageService,
    private router: Router,
    private categoryService: CategoryService,
    private articleService: ArticleService,
    private authService: NbAuthService) {
    this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
        if (!this.user.IsValid) {
          this.router.navigate(['login']);
        }
      });
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      title: [null, [Validators.required, Validators.maxLength(50)]],
      summary: [null, [Validators.required, Validators.maxLength(128)]],
      content: [null, [Validators.required, Validators.minLength(15)]],
      categoryId: [null, [Validators.required]],
      keywords: [null, [Validators.required, Validators.maxLength(50)]],
      canComment: [true]
    });
  }

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }

    if (!this.validateForm.valid) { return; }

    this.articleService.addArticle(this.validateForm.value)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            this.router.navigate(['/article', resp.result.body]);
          } else {
            if (resp.result.errors && resp.result.errors.length > 0) {
              let errorMsg = '';
              resp.result.errors.map((error, index, obj) => {
                errorMsg += '<br/>';
                errorMsg += error.description;
              });
              this.msg.error(`发布文章失败: ${errorMsg}`);
              return;
            }
            this.msg.error('发布文章失败,未知错误');
          }
        } else {
          this.msg.error('请求异常');
        }
      });
  }

  // saveDraft(e: MouseEvent): void {
  //   e.preventDefault();

  //   // tslint:disable-next-line:forin
  //   for (const i in this.validateForm.controls) {
  //     this.validateForm.controls[i].markAsDirty();
  //     this.validateForm.controls[i].updateValueAndValidity();
  //   }

  //   if (!this.validateForm.valid) { return; }

  //   this.articleService.addDraft(this.validateForm.value)
  //     .subscribe(resp => {
  //       if (resp.status === 200) {
  //         this.msg.success('提交成功');
  //       } else {
  //         this.msg.error('请求异常');
  //       }
  //     });
  // }

  ngAfterViewInit(): void {
    const elToolbar = this.el.nativeElement.querySelector('#toolbar');
    const elEditor = this.el.nativeElement.querySelector('#editor');
    this.editor = new wangEditor(elToolbar, elEditor);
    this.editor.customConfig.uploadImgShowBase64 = true;

    const self = this;
    this.editor.customConfig.onchange = function (html) {
      const content = self.validateForm.get('content') as FormControl;
      content.setValue(xss(html));
      console.warn(self.validateForm.value);
    };

    this.editor.create();

    this.categoryService.all()
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            this.categories = resp.result.body;
          }
        }
      });
  }

  clickHandle(): any {
    return this.editor.txt.text();
  }

  onChange($event: string[]): void {
    console.log($event);
  }
}
