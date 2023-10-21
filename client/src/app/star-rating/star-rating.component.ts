import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.scss']
})
export class StarRatingComponent implements OnInit {
  @Output() ratingSelected = new EventEmitter<number>();
  @Input() reviewRating: number;

  constructor() { }

  ngOnInit(): void {
  }

  onRatingSelected(rating: number) {
    this.ratingSelected.emit(rating);
  }
}
