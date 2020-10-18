import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: "root",
})
export class CategoriesService {
    constructor(private http: HttpClient) {}

    public getCategories() {
        return this.http.get("api/categories");
    }
}
