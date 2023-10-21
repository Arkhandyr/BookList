import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'star-rating-dynamic',
  templateUrl: './star-rating-dynamic.component.html',
  styleUrls: ['./star-rating-dynamic.component.scss']
})
export class StarRatingDynamicComponent implements OnInit {
  @Output() ratingSelected = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  onRatingSelected(rating: number) {
    this.ratingSelected.emit(rating);
  }
}
