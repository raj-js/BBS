import { Component, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  template: `
    {{value|date:'yyyy-MM-dd'}}
  `,
})
export class DateRenderComponent implements ViewCell, OnInit {

  value: string | number;
  rowData: any;

  constructor() { }

  ngOnInit() {
  }

}
