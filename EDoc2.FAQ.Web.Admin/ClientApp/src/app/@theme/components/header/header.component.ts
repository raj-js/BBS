import { Component, Input, OnInit } from '@angular/core';

import { NbMenuService, NbSidebarService } from '@nebular/theme';
import { AnalyticsService } from '../../../@core/utils/analytics.service';
import { LayoutService } from '../../../@core/data/layout.service';
import { NbAuthService, NbAuthJWTToken } from '@nebular/auth';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'ngx-header',
  styleUrls: ['./header.component.scss'],
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {

  @Input() position = 'normal';

  user = {};

  userMenu = [{ title: "注销", data: "logout" }];

  constructor(private sidebarService: NbSidebarService,
              private menuService: NbMenuService,
              private analyticsService: AnalyticsService,
              private layoutService: LayoutService,
              private authService: NbAuthService) {
                
                this.authService.onTokenChange()
                .subscribe((token: NbAuthJWTToken)=>{
                  if(token.isValid()){
                    this.user = token.getPayload();
                    console.log(this.user);
                  }
                });
  }

  ngOnInit() {
    this.menuService.onItemClick()
    .pipe(
      filter(({ tag }) => tag === 'user-avatar-menu'),
      map(({ item: { data } }) => data),
    )
    .subscribe(data=>{
      console.log(data);
      switch(data){
        case "logout":{
          this.authService.logout("email")
          .subscribe(result=>{
            console.log(result);         
          });
          break;
        }
      }
    });
  }

  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, 'menu-sidebar');
    this.layoutService.changeLayoutSize();

    return false;
  }

  toggleSettings(): boolean {
    this.sidebarService.toggle(false, 'settings-sidebar');

    return false;
  }

  goToHome() {
    this.menuService.navigateHome();
  }

  startSearch() {
    this.analyticsService.trackEvent('startSearch');
  }
}
