import { Component, Input, OnInit } from '@angular/core';
import { Book } from '../interfaces/Book';

@Component({
  selector: 'app-expandable-paragraph',
  templateUrl: './expandable-paragraph.component.html',
  styleUrls: ['./expandable-paragraph.component.scss']
})
export class ExpandableParagraphComponent implements OnInit {
  @Input()
  text : string
  synopsis : string
  buttonText : string
  expanded : boolean = true

  ngOnInit() {
    this.toggleParagraph()
  }

  toggleParagraph() {
    if(this.expanded) {
      this.synopsis = this.text.substring(0, 400)
      this.buttonText = "... ver mais"
    } else {
      this.synopsis = this.text
      this.buttonText = "... ver menos"
    }
    this.expanded = !this.expanded
  }
}