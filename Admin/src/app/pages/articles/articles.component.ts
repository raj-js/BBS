import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'ngx-articles',
  template: `
  <router-outlet></router-outlet>
  `
})
export class ArticlesComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
