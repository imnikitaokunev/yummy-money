import { QueryService } from "./query.service";
import { ExpenseRequest } from "../models/expense-request";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Expense } from "../models/expense";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { identifierModuleUrl } from "@angular/compiler";

@Injectable({
    providedIn: "root",
})
export class ExpensesService {
    constructor(private http: HttpClient, private queryService: QueryService) {}

    public getExpenses(request: ExpenseRequest): Observable<Expense[]> {
        let queryString = this.queryService.toQueryString(request);
        return this.http
            .get(`api/expenses?includes=Category&${queryString}`)
            .pipe(map((data: any) => data.map((x: any) => new Expense(x))));
    }

    public postExpense(expense: Expense) {
        return this.http.post(`api/expenses`, expense);
    }

    public removeExpense(id: number) {
        return this.http.delete(`api/expenses/${id}`);
    }
}
