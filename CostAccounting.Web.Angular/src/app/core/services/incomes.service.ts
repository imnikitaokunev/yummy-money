import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { Income } from "../models/income";
import { IncomeRequest } from "../models/income-request";
import { QueryService } from "./query.service";

@Injectable({
    providedIn: "root",
})
export class IncomesService {
    constructor(private http: HttpClient, private queryService: QueryService) {}

    public getIncomes(request: IncomeRequest): Observable<Income[]> {
        let queryString = this.queryService.toQueryString(request);
        return this.http
            .get(`api/incomes?includes=Category&${queryString}`)
            .pipe(map((data: any) => data.map((x: any) => new Income(x))));
    }
}
