import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { NavComponent } from './pages/nav/nav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BookAddComponent } from './pages/catalog/book-add/book-add.component';
import { BookListComponent } from './pages/catalog/book-list/book-list.component';
import { FormsBookResolver } from './pages/shared/forms-book.resolver';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import {
  MatAutocompleteModule,
  MatBadgeModule,
  MatBottomSheetModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
  MatTreeModule,
} from '@angular/material';
import { LoginComponent } from './pages/login/login.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { BookComponent } from './pages/book/book.component';
import { ExpandableParagraphComponent } from './expandable-paragraph/expandable-paragraph.component';
import { RegisterComponent } from './pages/register/register.component';
import { StarRatingDynamicComponent } from './star-rating/star-rating-dynamic/star-rating-dynamic.component';
import { SearchComponent } from './search/search.component';
import { BookGroupComponent } from './book-group/book-group.component';
import { StarRatingStaticComponent } from './star-rating/star-rating-static/star-rating-static.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';

@NgModule({
  declarations: [
    NavComponent,
    BookAddComponent,
    BookListComponent,
    LoginComponent,
    UserProfileComponent,
    BookComponent,
    ExpandableParagraphComponent,
    RegisterComponent,
    StarRatingDynamicComponent,
    SearchComponent,
    BookGroupComponent,
    StarRatingStaticComponent,
    LandingPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
  ],
  providers: [
    FormsBookResolver,
    NavComponent
  ],
  bootstrap: [NavComponent]
})
export class AppModule { }
