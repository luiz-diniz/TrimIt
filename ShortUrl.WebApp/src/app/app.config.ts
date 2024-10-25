import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { ngxLoadingAnimationTypes, NgxLoadingModule } from 'ngx-loading';
import { provideToastr } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    importProvidersFrom(NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.circleSwish,
      primaryColour: '#ffffff'
    })),
    provideAnimations(),
    provideToastr({
      timeOut: 2000,
      positionClass: 'toast-top-center'
    })
  ]
};
