import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class EnvironmentService {
  constructor(private http: HttpClient) {}

  public getEnvironment(): Observable<string> {
    return this.http.get("api/environment", { responseType: "text" });
  }
}
