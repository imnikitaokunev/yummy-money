import { EditTransactionComponent } from './../edit-transaction/edit-transaction.component';
import { AddTransactionComponent } from './../add-transaction/add-transaction.component';
import { catchError, finalize, map } from 'rxjs/operators';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, Input, OnInit } from '@angular/core';
import * as moment from 'moment';
import { Transaction } from 'src/app/core/models/transaction';
import { of } from 'rxjs';

@Component({
    selector: 'app-view-transactios',
    templateUrl: './view-transactios.component.html',
})
export class ViewTransactiosComponent implements OnInit {
    public isLoading: boolean;
    public isError: boolean;
    public isDeleting: boolean;
    public transactions: Transaction[] = [];

    @Input() date: Date;

    constructor(
        public activeModal: NgbActiveModal,
        public modalService: NgbModal,
        private apiHttpService: ApiHttpService,
        private apiEndpointsService: ApiEndpointsService
    ) {}

    ngOnInit(): void {
        this.load();
    }

    public get displayDate(): string {
        return moment(this.date).format('LL');
    }

    public get expensesSum(): number {
        return this.sum(this.transactions.filter((x) => !x.isIncome));
    }

    public get incomesSum(): number {
        return this.sum(this.transactions.filter((x) => x.isIncome));
    }

    public get net(): number {
        return this.incomesSum - this.expensesSum;
    }

    public get netPercent(): number {
        return this.incomesSum ? this.net / this.incomesSum : this.net ? -1 : 0;
    }

    public editTransaction(transaction: Transaction): void {
        let modal = this.modalService.open(EditTransactionComponent);
        modal.componentInstance.transaction = transaction;
        modal.closed.subscribe((result: Transaction) => this.load());
    }

    public deleteTransaction(transacation: Transaction): void {
        this.isDeleting = true;
      
        this.apiHttpService
            .delete(this.apiEndpointsService.deleteTransactionEndpoint(transacation.id))
            .pipe(finalize(() => (this.isDeleting = false)))
            .subscribe((response) => {
                this.transactions = this.transactions.filter(
                    (x) => x != transacation
                );
            });
    }

    public addTransaction(): void {
        let modal = this.modalService.open(AddTransactionComponent);
        modal.componentInstance.date = this.date;
        modal.closed.subscribe((result: Transaction) => this.load());
    }

    private load(): void {
        this.isLoading = true;
        let request = {
            startDate: moment(this.date).format('MM-DD-yyyy'),
            endDate: moment(this.date).add(1, 'days').format('MM-DD-yyyy'),
        };

        this.apiHttpService
            .get(this.apiEndpointsService.getTransactionsEndpoint(request))
            .pipe<Transaction[]>(
                map((data: any) => data.map((x: any) => new Transaction(x)))
            )
            .pipe(
                finalize(() => (this.isLoading = false)),
                catchError(() => {
                    this.isError = true;
                    return of([]);
                })
            )
            .subscribe((result) => {
                this.transactions = [].concat.apply([], result);
            });
    }

    private sum(array: { amount: number }[]): number {
        return array.reduce((sum, value) => sum + value.amount, 0);
    }
}
