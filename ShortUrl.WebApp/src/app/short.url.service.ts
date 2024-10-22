import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class ShortUrlService{
    private _httpClient = inject(HttpClient);

    create(url: string) : Observable<any>{
        return this._httpClient.post<any>("https://localhost:7124/url", {
            url: url
        });
    }
}