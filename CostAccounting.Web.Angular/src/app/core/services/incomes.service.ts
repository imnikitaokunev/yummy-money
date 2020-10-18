import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { Income } from "../models/income";

@Injectable({
    providedIn: "root",
})
export class IncomesService {
    constructor(private http: HttpClient) {}

    public getIncomes(startDate?: Date, endDate?: Date): Observable<Income[]> {
        return this.http
            .get("api/incomes?includes=Category")
            .pipe(map((data: any) => data.map((x: any) => new Income(x))));
    }
}
