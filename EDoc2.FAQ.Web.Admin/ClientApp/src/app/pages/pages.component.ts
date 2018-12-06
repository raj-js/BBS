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
    .subscribe(roles=>{
      console.log("111", roles,roles.indexOf(Roles.Administrator.Normalized));

      if(roles.indexOf(Roles.Administrator.Normalized) != -1){
        this.menu = MENU_ITEMS;
      }else if(roles.indexOf(Roles.Moderator.Normalized) != -1) {
        
      }
    });
  }
}
