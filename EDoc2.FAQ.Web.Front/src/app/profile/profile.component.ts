import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { User } from '../@core/User';
import { Router } from '@angular/router';
import { AccountService, ProfileResp, EditProfileReq } from '../@core/ApiProxy';
import { NzMessageService, UploadXHRArgs, UploadFile, NzIconService } from 'ng-zorro-antd';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Apis } from '../@core/Apis';
import { HttpRequest, HttpEventType, HttpEvent, HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  uploadAvatarApi = Apis.ModifyAvatar;
  avatarUrl = '';
  loading = false;

  user: User = null;
  profile: ProfileResp = null;
  profileForm: FormGroup;
  editProfileReq: EditProfileReq = new EditProfileReq();

  passwordForm: FormGroup;

  v = new Date().getTime().toString();

  constructor(
    private router: Router,
    private accountService: AccountService,
    private msg: NzMessageService,
    private authService: NbAuthService,
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private http: HttpClient) {
    this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
        if (!this.user.IsValid) {
          this.router.navigate(['/login']);
        }
        this.avatarUrl = Apis.AvatarUrl + this.user.Id;
      });

    this.profileForm = this.fb.group({
      id: ['', [Validators.required]],
      gender: [ 0, [] ],
      company: ['', [Validators.maxLength(30)]],
      city: ['', [Validators.maxLength(30)]],
      signature: ['', [Validators.maxLength(128)]]
    });

    this.passwordForm = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      newPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20) ] ],
      checkPassword: [ null, [ Validators.required, this.confirmationValidator ] ],
    });
  }

  confirmationValidator = (control: FormControl): { [ s: string ]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.passwordForm.controls.newPassword.value) {
      return { confirm: true, error: true };
    }
  }

  ngOnInit() {
    this.accountService.getProfile(this.user.Id)
      .subscribe(resp => {
        if (resp.status === 200) {
          if (resp.result.success) {
            this.profile = resp.result.body;
            this.profileForm.patchValue({
              id: this.user.Id,
              gender: `${this.profile.gender}`,
              company: this.profile.company,
              city: this.profile.city,
              signature: this.profile.signature
            });
            return;
          }
        } else {
          this.msg.error('网络异常');
        }
        this.router.navigate(['/login']);
      });
  }

  submitProfileForm = ($event, value) => {
    $event.preventDefault();
    // tslint:disable-next-line:forin
    for (const key in this.profileForm.controls) {
      this.profileForm.controls[key].markAsDirty();
      this.profileForm.controls[key].updateValueAndValidity();
    }
    if (!this.profileForm.valid) {return; }

    this.accountService.editProfile(value)
    .subscribe(resp => {
      if (resp.status === 200) {
        if (!!resp.result.success) {
          this.msg.success('修改个人信息成功');
        } else {
          if (resp.result.errors && resp.result.errors.length > 0) {
            let errorMsg = '';
            resp.result.errors.map((error, index, obj) => {
              errorMsg += '<br/>';
              errorMsg += error.description;
            });
            this.msg.error(`修改个人信息失败: ${errorMsg}`);
            return;
          }
          this.msg.error('修改个人信息失败,未知错误');
        }
      } else {
        this.msg.error('网络异常！');
      }
    });
  }

  goHomePage(e: MouseEvent): void {
    e.preventDefault();

    this.router.navigate(['person', this.user.Id]);
  }

  submitPasswordForm = ($event, value) => {
    $event.preventDefault();
    // tslint:disable-next-line:forin
    for (const key in this.passwordForm.controls) {
      this.passwordForm.controls[key].markAsDirty();
      this.passwordForm.controls[key].updateValueAndValidity();
    }

    if (!this.passwordForm.valid) {return; }

    this.accountService.modifyPassword(value)
    .subscribe(resp => {
      if (resp.status === 200) {
        if (!!resp.result.success) {
          this.msg.success('修改密码成功， 请重新登录！');
          setTimeout(() => {
            this.authService.logout('email')
            .subscribe(result => {
              if (result.isSuccess()) {
                if (result.getRedirect()) {
                  this.router.navigate(['/login']);
                }
              } else {
                this.msg.error('网络异常！');
              }
              this.cd.detectChanges();
            });
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
        this.msg.error('网络异常！');
      }
    });
  }

  customReq = (args: UploadXHRArgs) => {
    // 构建一个 FormData 对象，用于存储文件或其他参数
    const formData = new FormData();
    formData.append('file', args.file as any);
    const req = new HttpRequest('POST', args.action, formData, {
      reportProgress : true,
      headers: new HttpHeaders({
        Authorization : `bearer ${(Apis.Authorize.AuthToken)}`
      })
    });

    // 始终返回一个 `Subscription` 对象，nz-upload 会在适当时机自动取消订阅
    return this.http.request(req).subscribe((event: HttpEvent<{}>) => {
      if (event.type === HttpEventType.UploadProgress) {
        if (event.total > 0) {
          // tslint:disable-next-line:no-any
          (event as any).percent = event.loaded / event.total * 100;
        }
        // 处理上传进度条，必须指定 `percent` 属性来表示进度
        args.onProgress(event, args.file);
      } else if (event instanceof HttpResponse) {
        // 处理成功
        args.onSuccess(event.body, args.file, event);

        this.avatarUrl = this.avatarUrl.replace(`?v=${this.v}`, '');
        this.v = new Date().getTime().toString();
        this.avatarUrl = `${this.avatarUrl}?v=${this.v}`;
      }
    }, (err) => {
      // 处理失败
      args.onError(err, args.file);
    });
  }
}
