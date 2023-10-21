import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'star-rating-static',
  templateUrl: './star-rating-static.component.html',
  styleUrls: ['./star-rating-static.component.scss']
})
export class StarRatingStaticComponent implements OnInit {
  @Input() name: string;
  @Input() initialRating: number;
  rating: number;

  constructor() { }

  ngOnInit() {
    this.rating = this.initialRating;
  }

}
