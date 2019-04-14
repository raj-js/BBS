import { Component, OnInit, Input, Optional } from '@angular/core';

export class Filter {
  public filed: string;
  public title: string;
  public type: string;
  public enbale: boolean;
  public value: any;
  public list: any;

  constructor(filed: string, 
    title: string,
    type: string,
    enbale: boolean,
    value: any,
    list: any){
    
      this.filed = filed;
      this.title = title,
      this.type = type;
      this.enbale = enbale;
      this.value = value,
      this.list = list
  }

  toTableFilter(): any {
    return {
      field: this.filed,
      search: this.value
    };
  }
}

@Component({
  selector: 'ngx-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {

  @Input() filters: Filter[] = [];

  constructor() { }

  ngOnInit() {
  }

}

