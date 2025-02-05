import { Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShortUrlService } from './short.url.service';
import { HttpClientModule } from '@angular/common/http';
import { Clipboard } from '@angular/cdk/clipboard';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCopy } from '@fortawesome/free-solid-svg-icons';
import { NgxCaptchaModule, ReCaptcha2Component } from 'ngx-captcha';
import { NgxLoadingModule } from 'ngx-loading';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-short-url',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxCaptchaModule,
    FontAwesomeModule,
    NgxLoadingModule
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
  toastr = inject(ToastrService);

  @ViewChild(ReCaptcha2Component) reCaptcha!: ReCaptcha2Component;

  form: FormGroup;
  shortUrl?: string;
  loading?: boolean;

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
    const url = this.form.get('url')!.value;
    const response = this.form.get('recaptcha')!.value;

    this.shortUrl = '';
    this.loading = true;

    this.shortUrlService.create({
      url: url,
      captchaResponse: response
    }).subscribe({
      next: result => {
        this.shortUrl = `http://trim-it.great-site.net/${result.url}`;
        this.form.reset();
        this.reCaptcha.resetCaptcha();
        this.loading = false;
      },
      error: () => {
        this.toastr.error('Check your input or try again later', 'Error')
        this.reCaptcha.resetCaptcha();
        this.loading = false;        
      }
    })
  }

  copyShortUrl() {
    this.clipboard.copy(this.shortUrl!);
  }  
}
