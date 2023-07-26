import { Component } from '@angular/core';

@Component({
  selector: 'app-expandable-paragraph',
  templateUrl: './expandable-paragraph.component.html',
  styleUrls: ['./expandable-paragraph.component.scss']
})
export class ExpandableParagraphComponent {
  toggleParagraph() {
    const content = document.querySelector('.content') as HTMLElement;
    content.parentElement?.classList.toggle('expanded');
  }
}