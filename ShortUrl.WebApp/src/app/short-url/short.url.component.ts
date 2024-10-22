import { Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShortUrlService } from './short.url.service';
import { HttpClientModule } from '@angular/common/http';
import { Clipboard } from '@angular/cdk/clipboard';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCopy } from '@fortawesome/free-solid-svg-icons';
import { NgxCaptchaModule, ReCaptcha2Component } from 'ngx-captcha';

@Component({
  selector: 'app-short-url',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxCaptchaModule,
    FontAwesomeModule
  ],
  providers: [
    ShortUrlService
  ],
  templateUrl: './short.url.component.html',
  styleUrl: './short.url.component.scss'
})
export class ShortUrlComponent {

  shortUrlService = inject(ShortUrlService);
  clipboard = inject(Clipboard);

  @ViewChild(ReCaptcha2Component) reCaptcha!: ReCaptcha2Component;

  form: FormGroup;
  shortUrl?: string;

  faCopy = faCopy;

  get disabledSubmit(){
    return this.form.invalid;
  }

  constructor() {
    this.form = new FormGroup({
      url: new FormControl('', [Validators.required]),
      recaptcha: new FormControl('', [Validators.required])
    });    
  }

  onSubmit(){
    const url = this.form.get("url")!.value;
    const response = this.form.get("recaptcha")!.value;

    this.shortUrlService.create({
      url: url,
      response: response
    }).subscribe({
      next: result => {
        this.shortUrl = result.url;
        this.form.reset();
        this.reCaptcha.resetCaptcha();
      },
      error: result => {
        console.error(result.error);
      }
    })
  }

  copyShortUrl() {
    this.clipboard.copy(this.shortUrl!);
  }  
}
