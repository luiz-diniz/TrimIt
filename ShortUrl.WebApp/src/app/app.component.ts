import { HttpClientModule } from "@angular/common/http";
import { Component } from "@angular/core";
import { RouterModule } from "@angular/router";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    imports: [
        RouterModule,
        HttpClientModule
    ]
})
export class AppComponent{
}