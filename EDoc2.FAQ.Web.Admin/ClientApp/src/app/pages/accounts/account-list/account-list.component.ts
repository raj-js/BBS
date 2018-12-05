import { Component, Injectable } from '@angular/core';
import { ServerDataSource } from 'ng2-smart-table';
import { BoolRenderComponent } from '../../../@theme/components/renders/bool-render/bool-render.component';
import { DateRenderComponent } from '../../../@theme/components/renders/date-render/date-render.component';
import { ServerSourceConf } from 'ng2-smart-table/lib/data-source/server/server-source.conf';
import { HttpClient } from '@angular/common/http';
import { Apis } from '../../../@core/data/Config';

@Component({
  selector: 'ngx-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss']
})
@Injectable({
  providedIn: 'root'
})
export class AccountListComponent {
  title: string = "会员管理";

  settings = {
    selectMode: 'single',
    noDataMessage: '暂无数据',
    actions: {
      columnTitle: "",
      add: false,
      edit: false,
      delete: false,
      position: "left",
      class: "action-column",
      custom: [
        {
          name: 'mute',
          title: '<i class="nb-edit" title="屏蔽"></i>'
        }
      ]
    },
    columns: {
      nickname: {
        title: '昵称',
        type: 'string',
        filter: true,
      },
      email: {
        title: '邮箱',
        type: 'string',
        filter: true,
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

  constructor(private httpClient: HttpClient) {
    this.conf.endPoint = Apis.SearchUsers;
    this.conf.sortFieldKey = "OrderBy";
    this.conf.pagerPageKey = "PageIndex";
    this.conf.pagerLimitKey = "PageSize";
    this.conf.filterFieldKey = "#field#";
    this.conf.totalKey = "body.totalCount";
    this.conf.dataKey = "body.dtos";

    this.source = new ServerDataSource(this.httpClient, this.conf);
  }

  onCustom(event) {
    alert(`Custom event '${event.action}' fired on row №: ${event.data.id}`)
  }
}
