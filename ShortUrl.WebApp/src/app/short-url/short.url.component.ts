import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShortUrlService } from './short.url.service';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-short-url',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    ShortUrlService
  ],
  templateUrl: './short.url.component.html',
  styleUrl: './short.url.component.scss'
})
export class ShortUrlComponent {

  shortUrlService = inject(ShortUrlService);

  form: FormGroup;
  shortUrl?: string;

  constructor() {
    this.form = new FormGroup({
      url: new FormControl('', [Validators.required]),
    });    
  }

  onSubmit(){
    const url = this.form.get("url")!.value;

    this.shortUrlService.create(url).subscribe({
      next: result => {
        this.shortUrl = result.url;
      },
      error: result => {
        console.error(result.error);
      }
    })
  }

  get disabledSubmit(){
    return this.form.invalid;
  }
}