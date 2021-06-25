import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
    providedIn: "root",
})
export class ReportService {
    private url = 'https://localhost:44354/api/reports';

    constructor(private http: HttpClient) {}

    public download(data: any[]) {
        return this.http.post(`${this.url}`, data, {responseType: 'blob'});
    }
}
