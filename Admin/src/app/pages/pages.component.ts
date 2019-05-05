import { Component } from '@angular/core';

import { MENU_ITEMS } from './pages-menu';
import { NbAclService, NbRoleProvider } from '@nebular/security';
import { Roles } from '../@core/data/Config';
import { NbMenuItem } from '@nebular/theme';

@Component({
  selector: 'ngx-pages',
  styleUrls: ['pages.component.scss'],
  template: `
    <ngx-sample-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-sample-layout>
  `,
})
export class PagesComponent {

  menu: NbMenuItem[] = [];

  constructor(private aclService: NbAclService,
    private roleProvider: NbRoleProvider) {

    this.roleProvider.getRole()
    .subscribe(roles => {
      // 下面的权限菜单判断应该使用服务器请求， 鉴于系统权限模块相对简单， 所以直接使用 hardcoded
      if (roles.indexOf(Roles.Administrator.Normalized) !== -1) {
        this.menu = MENU_ITEMS;
      }else if (roles.indexOf(Roles.Moderator.Normalized) !== -1) {

      }
    });
  }
}
