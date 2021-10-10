import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { ApiEndpointsService } from './services/api-endpoints.service';
import { ApiHttpService } from './services/api-http.service';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { HttpErrorInterceptor } from './interceptors/http-error-interceptor.service';
import { ToastsComponent } from './components/toasts/toasts.component';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [CommonModule, NgbToastModule, HttpClientModule, RouterModule],
    declarations: [HeaderComponent, ToastsComponent],
    providers: [
        ApiHttpService,
        ApiEndpointsService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpErrorInterceptor,
            multi: true,
        },
    ],
    exports: [HeaderComponent, ToastsComponent],
})
export class CoreModule {}
