import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root",
})
export class QueryService {
    constructor() {}

    public toQueryString(model: any): string {
        let httpParams = new HttpParams();

        Object.keys(model).forEach(function (key) {
            let value = model[key];

            if (value instanceof Date) {
                value = value.toISOString();
            }

            httpParams = httpParams.set(key, value.toString());
        });

        return httpParams.toString();
    }
}
