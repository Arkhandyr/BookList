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
  options: number[] = [5, 4.5, 4, 3.5, 3, 2.5, 2, 1.5, 1, 0.5];

  constructor() { }

  ngOnInit() {
    this.rating = this.initialRating;
  }

  getRadioStyle(option: number) {
    return {
      color: this.initialRating >= option ? 'yellow' : 'white'
    };
  }
}
