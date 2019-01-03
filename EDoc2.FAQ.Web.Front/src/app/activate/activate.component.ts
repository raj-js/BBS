import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { EmailConfirmReq, AccountService } from '../@core/ApiProxy';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-activate',
  templateUrl: './activate.component.html',
  styleUrls: ['./activate.component.scss']
})
export class ActivateComponent implements OnInit {

  req: EmailConfirmReq = new EmailConfirmReq();
  activating = true;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private msg: NzMessageService) { }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.req.userId = params.get('userId');
        this.req.code = params.get('code');
        return this.accountService.emailConfrim(this.req);
      })
    ).subscribe(resp => {
      this.activating = false;
      if (resp.status === 200) {
        if (resp.result.success) {
          this.msg.error('激活成功！正在跳转登录页...');
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 1000);
        } else {
          if (resp.result.errors && resp.result.errors.length > 0) {
            let errorMsg = '';
            resp.result.errors.map((error, index, obj) => {
              errorMsg += '<br/>';
              errorMsg += error.description;
            });
            this.msg.error(`激活失败: ${errorMsg}`);
            return;
          }
          this.msg.error('激活失败,未知错误');
        }
      } else {
        this.msg.error('网络异常');
      }
    });
  }

}
