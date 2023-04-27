import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Book } from '../../../interfaces/IBook';
import { BookService } from '../../shared/books.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-add',
  templateUrl: './book-add.component.html',
  styleUrls: ['./book-add.component.scss']
})
export class BookAddComponent implements OnInit {

  constructor(
    private service:BookService, 
    private formBuilder:FormBuilder, 
    private toastr: ToastrService) { }

  form:FormGroup;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      "title": new FormControl(null, [Validators.required]),
      "author": new FormControl(null, [Validators.required]),
      "pages": new FormControl(null, [Validators.required]),
      "cover": new FormControl(null, [Validators.required])
    });
  }

  cover:string | ArrayBuffer | null;

  public selectedCover(imageInput: HTMLInputElement) {

    const reader = new FileReader();
    const files = imageInput.files as FileList;

    reader.addEventListener('load', (event: any) => {

      var element = event.target as FileReader;

      this.cover = element.result;
    });

    reader.readAsDataURL(files[0]);
  }

  submitData() {
    let book = this.form.value as Book;
    book.cover = this.cover as string;

    this.service.addBook(book)
      .subscribe(response => {
        this.toastr.success('Livro adicionado com sucesso', 'Sucesso');
        console.log(response)
      })
  }
}
