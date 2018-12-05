import { Component, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  template: `
    <nb-checkbox status="success" [(ngModel)]="value" disabled></nb-checkbox>
  `,
})
export class BoolRenderComponent implements ViewCell, OnInit {
  
  value: string | number;
  rowData: any;

  constructor() { }

  ngOnInit() {
  }
}
