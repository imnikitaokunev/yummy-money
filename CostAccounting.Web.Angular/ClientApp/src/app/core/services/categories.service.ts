import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Category } from "../models/category";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
    providedIn: "root",
})
export class CategoriesService {
    constructor(private http: HttpClient) {}

    public getCategories(): Observable<Category[]> {
        return this.http.get("api/categories").pipe(map((data: any) => data.map((x: any) => new Category(x))));
    }
}
