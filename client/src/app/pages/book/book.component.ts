import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Emitters } from 'src/app/emitters/emitters';
import { Book } from 'src/app/interfaces/Book';
import { BookService } from 'src/app/services/books.service';
import { ListService } from 'src/app/services/list.service';
import { NavComponent } from '../nav/nav.component';
import { HttpParams } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Review } from 'src/app/interfaces/Review';
import { ReviewService } from 'src/app/services/review.service';
import { UserService } from 'src/app/services/user.service';
import { Profile } from 'src/app/interfaces/Profile';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  username: string = this.navComponent.authenticatedUser;
  bookId: string;
  bookStatus: string;
  public book: Book;
  public reviews: Review[];
  private sub: any;
  public user: Profile;
  public userReview: Review[];
  public reviewText: string;
  selectedRating: number;

  constructor(
    private route: ActivatedRoute,
    private navComponent: NavComponent,
    private bookService: BookService,
    private listService: ListService,
    private userService: UserService,
    private reviewService: ReviewService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
       this.bookId = params['id'];

       this.bookService.getBookById(this.bookId).subscribe(x => this.book = x);

       this.listService.getBookStatus(this.bookId, this.username).subscribe(x => this.bookStatus = x);

       this.reviewService.getBookReviews(this.bookId).subscribe(x => this.reviews = x);

       this.userService.getByUsername(this.username).subscribe(x => this.user = x)

       this.userReview = this.reviews.filter((review) => review.user.username === this.username)
    });
  }

  addToList(listName: string): void {
    let listEntry : string = JSON.stringify({ Username: this.username, BookId: this.bookId, ListName: listName })

    this.listService.addToList(listEntry).subscribe({
      next: () => {
        this.listService.getBookStatus(this.bookId, this.username).subscribe(x => this.bookStatus = x)
        this.toastr.success('Livro adicionado com sucesso à lista', 'Sucesso');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  removeFromList(): void {
    let listEntry : string = JSON.stringify({ Username: this.username, BookId: this.bookId, ListName: '' })

    this.listService.removeFromList(listEntry).subscribe({
      next: () => {
        this.listService.getBookStatus(this.bookId, this.username).subscribe(x => this.bookStatus = x)
        this.toastr.success('Livro removido com sucesso da lista', 'Sucesso');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  addReview(): void {
    let reviewEntry : string = JSON.stringify({ Username: this.username, BookId: this.bookId, Text: this.reviewText, Rating: Number(this.selectedRating) })

    this.reviewService.addReview(reviewEntry).subscribe({
      next: () => {
        this.reviewService.getBookReviews(this.bookId).subscribe(x => this.reviews = x);
        this.toastr.success('Resenha publicada com sucesso!', 'Sucesso');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  deleteReview(): void {
    let reviewEntry : string = JSON.stringify({ Username: this.username, BookId: this.bookId })

    this.reviewService.deleteReview(reviewEntry).subscribe({
      next: () => {
        this.reviewService.getBookReviews(this.bookId).subscribe(x => this.reviews = x);
        this.toastr.success('Resenha removida com sucesso!', 'Sucesso');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  likeReview(review: Review): void {
    let likeEntry : string = JSON.stringify({ Username: this.username, ReviewId: review._id})

    if(!review.likes.includes(this.username)) {
      this.reviewService.likeReview(likeEntry).subscribe({
        next: () => {
          this.reviewService.getBookReviews(this.bookId).subscribe(x => this.reviews = x);
        },
        error: (err) => {
          console.log(err);
        }
      });
    }
    else {
      this.reviewService.dislikeReview(likeEntry).subscribe({
        next: () => {
          this.reviewService.getBookReviews(this.bookId).subscribe(x => this.reviews = x);
        },
        error: (err) => {
          console.log(err);
        }
      });
    }
  }

  onRatingSelected(rating: number) {
    this.selectedRating = rating;
  }

  averageRating(reviewRatings: Review[]): number {
    if (reviewRatings.length === 0) {
      return 0;
    }
  
    const sum = reviewRatings.reduce((acc, current) => acc + current.rating, 0);
    return Math.round(sum / reviewRatings.length * 10) / 10;
  }
}
