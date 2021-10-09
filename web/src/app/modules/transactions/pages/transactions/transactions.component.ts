import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { Moment } from 'moment';
import { of } from 'rxjs';
import { catchError, finalize, map } from 'rxjs/operators';
import { Transaction } from 'src/app/core/models/transaction';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { AddTransactionComponent } from 'src/app/modules/transactions/components/add-transaction/add-transaction.component';

@Component({
    selector: 'app-transactions',
    templateUrl: './transactions.component.html',
})
export class TransactionsComponent implements OnInit, OnChanges {
    public isLoading: boolean = true;
    public isError: boolean;
    public data: Transaction[] = [];
    public transactions: Transaction[] = [];

    // Filtering
    public showFilter: boolean;
    public minAmount: number;
    public maxAmount: number;
    public showExpenses: boolean = true;
    public showIncomes: boolean = true;

    public currentDate: Moment;
    public firstDay: Moment;
    public lastDay: Moment;

    constructor(
        private apiEndpointsService: ApiEndpointsService,
        private apiHttpService: ApiHttpService,
        private modalService: NgbModal
    ) {}

    ngOnInit(): void {
        this.currentDate = moment();
        this.updateRange();
        this.loadData();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.data) {
            this.filterTransactions();
        }
    }

    public prevMonth(): void {
        this.currentDate = moment(this.currentDate).subtract(1, 'months');
        this.updateRange();
        this.loadData();
    }

    public nextMonth(): void {
        this.currentDate = moment(this.currentDate).add(1, 'months');
        this.updateRange();
        this.loadData();
    }

    public getMonthName(offset: number): string {
        return this.currentDate.clone().add(offset, 'months').format('MMMM');
    }

    public addTransaction(): void {
        this.modalService.open(AddTransactionComponent);
    }

    public filterTransactions(event?: any): void {
        this.transactions = [];
        if (this.showExpenses) {
            this.transactions = this.transactions.concat(
                this.data.filter((x) => !x.isIncome)
            );
        }
        if (this.showIncomes) {
            this.transactions = this.transactions.concat(
                this.data.filter((x) => x.isIncome)
            );
        }
    }

    public loadData(): void {
        this.isError = false;
        this.isLoading = true;

        let request = {
            startDate: moment(this.firstDay)
                .add(1, 'days')
                .toISOString()
                .slice(0, 10),
            endDate: moment(this.lastDay).toISOString().slice(0, 10),
            minAmount: this.minAmount,
            maxAmount: this.maxAmount,
        };

        this.apiHttpService
            .get(this.apiEndpointsService.getTransactionsEndpoint(request))
            .pipe(
                finalize(() => (this.isLoading = false)),
                catchError(() => {
                    this.isError = true;
                    return of([]);
                })
            )
            .pipe<Transaction[]>(
                map((data: any) => data.map((x: any) => new Transaction(x)))
            )
            .subscribe((result) => {
                this.data = [].concat.apply([], result);
                this.filterTransactions();
            });
    }

    private updateRange(): void {
        const firstOfMonth = moment(this.currentDate).startOf('month').day();
        const lastOfMonth = moment(this.currentDate).endOf('month').day();

        this.firstDay = moment(this.currentDate)
            .startOf('month')
            .subtract(firstOfMonth, 'days');
        this.lastDay = moment(this.currentDate)
            .endOf('month')
            .subtract(lastOfMonth, 'days')
            .add(7, 'days');
    }
}
