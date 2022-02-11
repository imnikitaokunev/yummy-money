import { ListTransaction } from 'src/app/core/models/list-transaction';
import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { TransactionsList } from 'src/app/core/models/transactions-list';
import { Component, OnInit } from '@angular/core';
import { map, finalize, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
    public data: TransactionsList;
    public isError: boolean;
    public isLoading: boolean;
    public transactionsList: ListTransaction[] = [];

    public readonly defaultPageSize: number = 15;
    public readonly defaultPageMultiplier: number = 2;

    constructor(
        private apiEndpointsService: ApiEndpointsService,
        private apiHttpService: ApiHttpService
    ) {}

    ngOnInit(): void {
        this.load();
    }

    public get transactions(): ListTransaction[] {
        return this.data.items;
    }

    public get isAllSelected(): boolean {
        return (
            this.data &&
            this.data.items &&
            this.data.items.length === this.selectedCount
        );
    }

    public get partSelection(): boolean {
        return !this.isAllSelected && this.selectedCount > 0;
    }

    public get selectedCount(): number {
        return this.data
            ? this.data.items.filter((x) => x.isSelected).length
            : 0;
    }

    public load(): void {
        this.isLoading = true;

        let request = {
            pageNumber: 1,
            pageSize: this.data ? this.data.pageSize * this.defaultPageMultiplier : this.defaultPageSize,
        };
        this.apiHttpService
            .get(this.apiEndpointsService.getTransactionsPagedEndpoint(request))
            .pipe(map((data) => new TransactionsList(data)))
            .pipe(
                finalize(() => (this.isLoading = false)),
                catchError(() => {
                    this.isError = true;
                    return of(null);
                })
            )
            .subscribe((data) => {
                this.data = this.data ? this.data.update(data, true) : data;
            });
    }

    public selectAll(): void {
        let isSelected = !this.isAllSelected;
        this.transactions.forEach((x) => (x.isSelected = isSelected));
    }
}
