import { Component, Injectable } from '@angular/core';
import { ServerDataSource } from 'ng2-smart-table';
import { BoolRenderComponent } from '../../../@theme/components/renders/bool-render/bool-render.component';
import { DateRenderComponent } from '../../../@theme/components/renders/date-render/date-render.component';
import { ServerSourceConf } from 'ng2-smart-table/lib/data-source/server/server-source.conf';
import { HttpClient } from '@angular/common/http';
import { Apis } from '../../../@core/data/Config';
import { AdminService } from '../../../@core/data/ApiProxy';

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

  constructor(private httpClient: HttpClient, private adminService: AdminService) {
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
    switch(event.action){
      case "mute":{
        this.adminService.muteUser("1")
        .subscribe((resp)=>{
          if(resp.status == 200){
            if(resp.result.success){
              alert("操作成功");
            }else{
              alert(resp.result.errors.toString());
            }
          }
        });
        break;
      }
      case "unmute":{
        alert("取消屏蔽");
        break;
      }
    }
  }
}
