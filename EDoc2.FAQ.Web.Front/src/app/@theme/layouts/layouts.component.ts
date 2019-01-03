import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { NzMessageService } from 'ng-zorro-antd';
import { Router } from '@angular/router';
import { Apis } from 'src/app/@core/Apis';
import { User } from 'src/app/@core/User';

@Component({
  selector: 'app-layouts',
  templateUrl: './layouts.component.html',
  styleUrls: ['./layouts.component.css']
})
export class LayoutsComponent implements OnInit {

  user: User = null;
  token: NbAuthJWTToken = null;

  constructor(private authService: NbAuthService,
    private msg: NzMessageService,
    private router: Router,
    private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.authService.onTokenChange()
      .subscribe((token: NbAuthJWTToken) => {
        this.user = User.FromJwt(token);
      });
  }

  logout(): void {
    this.authService.logout('email')
      .subscribe(result => {
        if (result.isSuccess()) {
          if (result.getRedirect()) {
            this.router.navigateByUrl(result.getRedirect());
          }
        } else {
          this.msg.error('网络异常！');
        }
        this.cd.detectChanges();
      });
  }
}
