import { Component, OnInit } from '@angular/core';
import { BoolRenderComponent } from '../../../@theme/components/renders/bool-render/bool-render.component';
import { DateRenderComponent } from '../../../@theme/components/renders/date-render/date-render.component';
import { ServerSourceConf } from 'ng2-smart-table/lib/data-source/server/server-source.conf';
import { ServerDataSource } from 'ng2-smart-table';
import { HttpClient } from '@angular/common/http';
import { ArticleService } from '../../../@core/data/ApiProxy';
import { NbToastrService } from '@nebular/theme';
import { Apis } from '../../../@core/data/Config';
import { Filter } from '../../../@theme/components/filter/filter.component';

@Component({
  selector: 'ngx-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent {

  title: string = "文章管理 / 文章搜索";

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
          title: '<i class="nb-arrow-thin-up" title="置顶"></i>'
        },
        {
          name: 'unmute',
          title: '<i class="nb-arrow-thin-down" title="取消置顶"></i>'
        }
      ]
    },
    columns: {
      title: {
        title: '标题',
        type: 'string',
        filter: false,
      },
      keywords: {
        title: '关键字',
        type: 'string',
        filter: false,
      },
      type: {
        title: '类型',
        type: 'string',
        filter: false
      },
      state: {
        title: '状态',
        type: 'string',
        filter: false
      },
      likes: {
        title: '赞',
        type: 'int',
        filter: false,
      },
      dislikes: {
        title: '踩',
        type: 'int',
        filter: false,
      },
      pv: {
        title: '访问量',
        type: 'int',
        filter: false,
      },
      creatationTime: {
        title: '创建日期',
        type: 'custom',
        renderComponent: DateRenderComponent,
        filter: false,
      }
    },
    pager: {
      perPage: 15
    }
  }; 

  conf: ServerSourceConf = new ServerSourceConf();
  source: ServerDataSource;

  constructor(private http: HttpClient, 
    private articleService: ArticleService,
    private toastrService: NbToastrService) {
      
      this.articleService.types()
      .subscribe(resp=>{
        if(resp.status == 200){
          if(resp.result.success){
            this.filters.push(new Filter("type", "类型", "list", false, "", resp.result.body));
          } else {
            this.toastrService.danger(`加载文章类型失败！${resp.result.errors}`, "加载", { });
          }
        }else{
          this.toastrService.danger("加载文件类型失败！", `${resp.status}`, { });
        }
      });

      this.articleService.states()
      .subscribe(resp=>{
        if(resp.status == 200){
          if(resp.result.success){
            this.filters.push(new Filter("state", "状态", "list", false, "", resp.result.body));
          } else {
            this.toastrService.danger(`加载文章状态失败！${resp.result.errors}`, "加载", { });
          }
        }else{
          this.toastrService.danger("加载文件状态失败！", `${resp.status}`, { });
        }
      });

      this.conf.endPoint = `${Apis.SearchArticles}?${Apis.AccessTokenName}=${Apis.AccessToken}`;
      this.conf.sortFieldKey = "OrderBy";
      this.conf.pagerPageKey = "PageIndex";
      this.conf.pagerLimitKey = "PageSize";
      this.conf.filterFieldKey = "#field#";
      this.conf.totalKey = "body.totalCount";
      this.conf.dataKey = "body.dtos";

      this.source = new ServerDataSource(this.http, this.conf);
  }

  onCustom(event) {
    
  }

  toggle() {
    this.revealed = !this.revealed;
  }

  filters: Filter[] = [
    new Filter("title", "标题", "string", false, "", undefined),
    new Filter("keywords", "关键字", "string", false, "", undefined),
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
