import { Register } from "./../models/register";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Login } from "../models/login";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Router } from "@angular/router";
import { Token } from "../models/token";
import { tap } from "rxjs/operators";

export const ACCESS_TOKEN_KEY = "access_token";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {}

  public login(login: Login): Observable<any> {
    return this.http.post<Token>("api/login", login).pipe(
      tap((token) => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.token);
      })
    );
  }

  public isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  public logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate([""]);
  }

  public register(register: Register): Observable<any> {
    return this.http.post("api/register", register);
  }

  public getCurrentUser(): any {
    if(!this.isAuthenticated()){
      return null;
    }

    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    if(token == null){
      return null;
    }

    return this.jwtHelper.decodeToken(token);
  }
}
