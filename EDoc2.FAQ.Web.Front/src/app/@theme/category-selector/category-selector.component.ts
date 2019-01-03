import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CategoryService } from 'src/app/@core/ApiProxy';

@Component({
  selector: 'app-category-selector',
  templateUrl: './category-selector.component.html',
  styleUrls: ['./category-selector.component.scss']
})
export class CategorySelectorComponent implements OnInit {

  @Output() value: EventEmitter<any> = new EventEmitter();

  nodes: any = [];

  constructor(private categoryService: CategoryService) {
  }

  ngOnInit() {
    this.categoryService.all()
    .subscribe(resp => {
      if (resp.status === 200) {
        if (resp.result.success) {
          this.nodes = resp.result.body;
        }
      }
    });
  }

}
