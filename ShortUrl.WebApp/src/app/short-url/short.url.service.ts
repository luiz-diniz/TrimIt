import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable()
export class ShortUrlService{
    private _httpClient = inject(HttpClient);

    create(request: {url: string, response: string}) : Observable<any>{
        return this._httpClient.post<any>(environment.apiUrl, {
            url: request.url,
            response: request.response
        });
    }

    getUrl(shortUrl: string) : Observable<any>{
        return this._httpClient.get<any>(environment.apiUrl + shortUrl);
    }
}
