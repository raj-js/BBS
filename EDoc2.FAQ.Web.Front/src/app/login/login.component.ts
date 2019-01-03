import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';
import { NbAuthService } from '@nebular/auth';
import { Apis } from '../@core/Apis';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {
  validateForm: FormGroup;

  constructor(private fb: FormBuilder,
    private msg: NzMessageService,
    private router: Router,
    private authService: NbAuthService,
    private cd: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email: [null, [Validators.required]],
      password: [null, [Validators.required]],
      rememberMe: [true]
    });
  }

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }

    if (!this.validateForm.valid) { return; }

    this.authService.authenticate('email', this.validateForm.value)
      .subscribe(result => {
        if (result.isSuccess()) {
          if (result.getRedirect()) {
            Apis.Authorize.AuthToken = result.getToken().getValue();
            this.msg.success('登录成功， 即将跳转...');
            setTimeout(() => {
              this.router.navigateByUrl(result.getRedirect());
            }, 1000);
          }
        } else {
          this.msg.error('邮箱与密码不匹配！');
        }
        this.cd.detectChanges();
      });
  }
}
