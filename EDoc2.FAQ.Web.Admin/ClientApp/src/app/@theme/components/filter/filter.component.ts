import { Component, OnInit, Input } from '@angular/core';

export class Filter {
  public filed: string;
  public title: string;
  public type: string;
  public enbale: boolean;
  public value: any;
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

