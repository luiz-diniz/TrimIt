import { Routes } from '@angular/router';
import { ShortUrlComponent } from './short-url/short.url.component';
import { UrlRedirectComponent } from './url-redirect/url.redirect.component';

export const routes: Routes = [  
    { path: ':shortUrl', component: UrlRedirectComponent },
    { path: '', component: ShortUrlComponent },
    { path: '**', redirectTo: '' }
];
