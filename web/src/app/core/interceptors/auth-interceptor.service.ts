import { AuthService } from 'src/app/core/services/auth.service';
import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {}

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        let token = this.authService.currentToken;

        if (token !== null) {
            request = request.clone({
                headers: request.headers.set(
                    'Authorization',
                    `Bearer ${token}`
                ),
            });
        }

        return next.handle(request);
    }
}
