import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { AccountService, ResetPasswordReq, RetrievePasswordReq } from '../@core/ApiProxy';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.scss']
})
export class ForgotComponent implements OnInit {

  validateForm: FormGroup;

  req: RetrievePasswordReq = new RetrievePasswordReq();

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private msg: NzMessageService,
    private router: Router) {
  }

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }

    if (!this.validateForm.valid) { return; }

    this.accountService.retrievePassword(this.validateForm.value)
    .subscribe(resp => {
      if (resp.status === 200) {
        if (resp.result.success) {
          this.msg.success('邮件已发送至您填写的邮箱，请登录邮箱进行后续操作！');
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
            this.msg.error(`发送邮件失败: ${errorMsg}`);
            return;
          }
          this.msg.error('发送邮件失败,未知错误');
        }
      } else {
        this.msg.error('网络异常');
      }
    });
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email: [ null, [ Validators.required, Validators.email ] ],
    });
  }
}
