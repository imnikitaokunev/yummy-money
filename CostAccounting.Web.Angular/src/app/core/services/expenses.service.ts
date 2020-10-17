import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: "root",
})
export class ExpensesService {
    constructor(private http: HttpClient) {}

    public getExpenses(startDate?: Date, endDate?: Date) {
        return this.http.get("api/expenses");
    }
}
