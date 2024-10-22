import { Component, inject } from "@angular/core";
import { ShortUrlService } from "../short-url/short.url.service";
import { ActivatedRoute, Router } from "@angular/router";
import { catchError, of, switchMap } from "rxjs";


@Component({
    selector: 'app-url-redirect',
    standalone: true,
    template: '',
    providers: [
      ShortUrlService
    ]
})
export class UrlRedirectComponent{
    route = inject(ActivatedRoute);
    router = inject(Router);
    shortUrlService = inject(ShortUrlService);

    ngOnInit() : void{
      this.route.params.pipe(
        switchMap((params) => {
          const shortUrl = params['shortUrl'];
          return this.shortUrlService.getUrl(shortUrl);
        }), 
        catchError(error =>  {
          console.error(error);
          return of(null);
        })
      ).subscribe(result => {
        if (result && result.url) {
          window.location.href = result.url;
        } else {
          this.router.navigate(['']);
        }
      })
    }
}