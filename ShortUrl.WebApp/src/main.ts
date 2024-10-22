import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { ShortUrlComponent } from './app/short.url.component';

bootstrapApplication(ShortUrlComponent, appConfig)
  .catch((err) => console.error(err));
