import { ToastService } from './../services/toast.service';
import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpParams,
    HttpRequest,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { stringify } from 'querystring';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(private toastService: ToastService) {}

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                let errorMsg = '';

                if (error.error instanceof ErrorEvent) {
                    console.log('this is client side error');
                    errorMsg = `Error: ${error.error.message}`;
                } else {
                    console.log('this is server side error');
                    errorMsg = `Error Code: ${error.status},  Message: ${error.message}`;
                }

                // ToDo: Replace with header checking
                if (error.error?.title?.includes('validation')) {
                    return throwError(error);
                }

                this.toastService.show(errorMsg, {
                    classname: 'fw-bold bg-invalid user-select-none',
                });

                return throwError(errorMsg);
            })
        );
    }
}

// @Injectable()
// export class BasicAuthInterceptor implements HttpInterceptor {
//     intercept(
//         request: HttpRequest<any>,
//         next: HttpHandler
//     ): Observable<HttpEvent<any>> {
//         // add authorization header with basic auth credentials if available
//         // let currentUser = JSON.parse(localStorage.getItem('currentUser'));
//         // if (currentUser && currentUser.authdata) {
//         request = request.clone({
//             body: {
//                 ...request.body,
//                 userId: 'f2dce61f-828b-4310-0fd0-08d949626d84',
//             },
//         });
//         // }

//         return next.handle(request);
//     }
// }
