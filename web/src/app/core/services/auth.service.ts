import { Router } from '@angular/router';
import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { Injectable } from '@angular/core';
import { SignInRequest } from '../models/sign-in-request';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../models/user';

export const ACCESS_TOKEN_KEY = 'access_token';

export function tokenGetter() {
    return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private useSessionStorage: boolean;

    constructor(
        private apiEndpointsService: ApiEndpointsService,
        private apiHttpService: ApiHttpService,
        private jwtHelper: JwtHelperService,
        private router: Router
    ) {}

    public get isLoggedIn(): boolean {
        let authToken = this.storage.getItem(ACCESS_TOKEN_KEY);
        return authToken !== null ? true : false;
    }

    public get currentUser(): User {
        if (!this.isLoggedIn) {
            return null;
        }

        var token = this.storage.getItem(ACCESS_TOKEN_KEY);
        if (token == null) {
            return null;
        }

        return this.jwtHelper.decodeToken<User>(token);
    }

    private get storage(): Storage {
        if(sessionStorage.getItem(ACCESS_TOKEN_KEY)){
            this.useSessionStorage = true;
        }
        return this.useSessionStorage ? sessionStorage : localStorage;
    }

    public signIn(request: SignInRequest): Observable<any> {
        this.useSessionStorage = !request.stayLogged;
        return this.apiHttpService
            .post(this.apiEndpointsService.signInEndpoint(), request)
            .pipe(
                tap((result) => {
                    if (request.stayLogged) {
                        localStorage.setItem(ACCESS_TOKEN_KEY, result.token);
                    } else {
                        sessionStorage.setItem(ACCESS_TOKEN_KEY, result.token);
                    }
                    console.log(this.currentUser);
                })
            );
    }

    public signOut(): void {
        let removeToken = this.storage.removeItem(ACCESS_TOKEN_KEY);
        if (removeToken == null) {
            this.router.navigate(['']);
        }
    }
}
