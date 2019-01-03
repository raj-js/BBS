import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { AccountService } from '../@core/ApiProxy';
import { NzMessageService } from 'ng-zorro-antd';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  validateForm: FormGroup;

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private msg: NzMessageService) {
  }

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }

    if (!this.validateForm.valid) { return; }

    this.accountService.register(this.validateForm.value)
    .subscribe(resp => {
      if (resp.status === 200) {
        if (!!resp.result.success) {
          this.msg.success('激活邮件已发送至您填写的邮箱，请登录邮箱进行激活！');
          this.validateForm.reset();
        } else {
          if (resp.result.errors && resp.result.errors.length > 0) {
            let errorMsg = '';
            resp.result.errors.map((error, index, obj) => {
              errorMsg += '<br/>';
              errorMsg += error.description;
            });
            this.msg.error(`注册失败: ${errorMsg}`);
            return;
          }
          this.msg.error('注册失败,未知错误');
        }
      } else {
        this.msg.error('网络异常！');
      }
    });
  }

  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.validateForm.controls.checkPassword.updateValueAndValidity());
  }

  confirmationValidator = (control: FormControl): { [ s: string ]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
  }

  getCaptcha(e: MouseEvent): void {
    e.preventDefault();
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email            : [ null, [ Validators.email, Validators.required ] ],
      password         : [ null, [ Validators.required ] ],
      checkPassword    : [ null, [ Validators.required, this.confirmationValidator ] ],
      nickname         : [ null, [ Validators.required ] ],
      agree            : [ false ]
    });
  }
}
