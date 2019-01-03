import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-question-state',
  templateUrl: './question-state.component.html',
  styleUrls: ['./question-state.component.scss']
})
export class QuestionStateComponent implements OnInit {

  @Input() state = 1;

  @Input() text = '';

  constructor() { }

  ngOnInit() {
  }

}
