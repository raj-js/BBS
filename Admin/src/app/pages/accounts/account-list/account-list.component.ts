import { Component, Injectable, ChangeDetectionStrategy } from '@angular/core';
import { ServerDataSource } from 'ng2-smart-table';
import { BoolRenderComponent } from '../../../@theme/components/renders/bool-render/bool-render.component';
import { DateRenderComponent } from '../../../@theme/components/renders/date-render/date-render.component';
import { ServerSourceConf } from 'ng2-smart-table/lib/data-source/server/server-source.conf';
import { HttpClient } from '@angular/common/http';
import { Apis } from '../../../@core/data/Config';
import { AdminService } from '../../../@core/data/ApiProxy';
import { NbToastrService } from '@nebular/theme';
import { Filter } from '../../../@theme/components/filter/filter.component';

@Component({
  selector: 'ngx-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default
})
@Injectable({
  providedIn: 'root'
})
export class AccountListComponent {
  title: string = "会员管理";

  revealed: boolean = false;

  settings = {
    selectMode: 'single',
    noDataMessage: '暂无数据',
    actions: {
      columnTitle: "",
      add: false,
      edit: false,
      delete: false,
      position: "left",
      custom: [
        {
          name: 'mute',
          title: '<i class="nb-volume-mute" title="屏蔽"></i>'
        },
        {
          name: 'unmute',
          title: '<i class="nb-volume-high" title="取消屏蔽"></i>'
        }
      ]
    },
    columns: {
      nickname: {
        title: '昵称',
        type: 'string',
        filter: false,
      },
      email: {
        title: '邮箱',
        type: 'string',
        filter: false,
      },
      roleName: {
        title: '角色',
        type: 'string',
        filter: false,
      },
      emailConfirmed: {
        title: '邮箱是否确认',
        type: 'custom',
        filter: false,
        renderComponent: BoolRenderComponent,
      },
      isMuted: {
        title: '是否屏蔽',
        type: 'custom',
        filter: false,
        renderComponent: BoolRenderComponent,
      },
      joinDate: {
        title: '注册日期',
        type: 'custom',
        filter: false,
        renderComponent: DateRenderComponent,
      }
    },
    pager: {
      perPage: 15
    }
  };

  conf: ServerSourceConf = new ServerSourceConf();
  source: ServerDataSource;

  constructor(private http: HttpClient,
    private adminService: AdminService,
    private toastrService: NbToastrService) {

    this.conf.endPoint = `${Apis.SearchUsers}?${Apis.AccessTokenName}=${Apis.AccessToken}`;
    this.conf.sortFieldKey = "OrderBy";
    this.conf.pagerPageKey = "PageIndex";
    this.conf.pagerLimitKey = "PageSize";
    this.conf.filterFieldKey = "#field#";
    this.conf.totalKey = "body.totalCount";
    this.conf.dataKey = "body.dtos";

    this.source = new ServerDataSource(this.http, this.conf);
  }

  onCustom(event) {
    switch (event.action) {
      case "mute": {
        this.adminService.muteUser(event.data.id)
          .subscribe(resp => {
            if (resp.status == 200) {
              if (resp.result.success) {
                this.toastrService.success("操作成功！", "屏蔽", { icon: "nb-volume-mute" });
                this.source.refresh();
              } else {
                this.toastrService.danger(`${resp.result.errors}`, { icon: "nb-volume-mute" });
              }
            } else {
              this.toastrService.danger("操作异常！", `${resp.status}`, { icon: "nb-volume-mute" });
            }
          });
        break;
      }
      case "unmute": {
        this.adminService.unmuteUser(event.data.id)
          .subscribe(resp => {
            if (resp.status == 200) {
              if (resp.status == 200) {
                if (resp.result.success) {
                  this.toastrService.success("操作成功！", "取消屏蔽", { icon: "nb-volume-high" });
                  this.source.refresh();
                } else {
                  this.toastrService.danger(`${resp.result.errors}`, "取消屏蔽", { icon: "nb-volume-high" });
                }
              } else {
                this.toastrService.danger("操作异常！", `${resp.status}`, { icon: "nb-volume-high" });
              }
            }
          });
        break;
      }
    }
  }

  toggle() {
    this.revealed = !this.revealed;
  }

  filters: Filter[] = [
    new Filter("nickname", "昵称", "string", false, "", undefined),
    new Filter("email", "邮箱", "string", false, "", undefined),
    new Filter("emailConfirmed", "邮箱是否确认", "string", false, "", undefined),
    new Filter("isMuted", "是否屏蔽", "string", false, "", undefined),
  ];

  search() {
    let mapFilters = this.filters
      .filter(filter => filter.enbale)
      .map(filter => filter.toTableFilter());
    this.source.setFilter(mapFilters);

    this.toggle();
  }

  cancel() {
    this.toggle();
  }
}
