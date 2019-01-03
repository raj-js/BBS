import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControl
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService, ResetPasswordReq } from '../@core/ApiProxy';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.scss']
})
export class ResetComponent implements OnInit {
  validateForm: FormGroup;

  req: ResetPasswordReq = new ResetPasswordReq();

  confirmationValidator = (control: FormControl): { [ s: string ]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
  }

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }

    if (!this.validateForm.valid) { return; }

    this.req.password = this.validateForm.get('password').value;
    this.accountService.resetPassword(this.req)
    .subscribe(resp => {
      if (resp.status === 200) {
        if (resp.result.success) {
          this.msg.success('修改密码成功！将跳转登录页...');
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
            this.msg.error(`修改密码失败: ${errorMsg}`);
            return;
          }
          this.msg.error('修改密码失败,未知错误');
        }
      } else {
        this.msg.error('网络异常');
      }
    });
  }

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private msg: NzMessageService) {
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      password         : [ null, [ Validators.required, Validators.minLength(6), Validators.maxLength(20) ] ],
      checkPassword    : [ null, [ Validators.required, this.confirmationValidator ] ],
    });

    this.route.paramMap.subscribe(params => {
      this.req.userId = params.get('userId');
      this.req.code = params.get('code');
    });
  }
}
