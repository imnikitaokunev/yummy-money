import { RouterModule } from '@angular/router';
import { ApiEndpointsService } from './services/api-endpoints.service';
import { ApiHttpService } from './services/api-http.service';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { HttpErrorInterceptor } from './interceptors/http-error-interceptor.service';

@NgModule({
    imports: [HttpClientModule, RouterModule],
    declarations: [HeaderComponent],
    providers: [ApiHttpService, ApiEndpointsService, {
          provide: HTTP_INTERCEPTORS,
          useClass: HttpErrorInterceptor,
          multi: true
        }
    ],
    exports: [HeaderComponent],
})
export class CoreModule {}
