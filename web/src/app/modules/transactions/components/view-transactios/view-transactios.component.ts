import { finalize, map } from 'rxjs/operators';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, Input, OnInit } from '@angular/core';
import * as moment from 'moment';
import { Transaction } from 'src/app/core/models/transaction';
import { Expense } from 'src/app/core/models/expense';
import { Income } from 'src/app/core/models/income';
import { combineLatest, forkJoin, Observable } from 'rxjs';

@Component({
    selector: 'app-view-transactios',
    templateUrl: './view-transactios.component.html',
})
export class ViewTransactiosComponent implements OnInit {
    public isLoading: boolean;
    public transactions: Transaction[] = [];
    public data: Observable<Transaction[]> | undefined;

    @Input() date: Date;

    constructor(
        public activeModal: NgbActiveModal,
        private apiHttpService: ApiHttpService,
        private apiEndpointsService: ApiEndpointsService
    ) {}

    ngOnInit(): void {
        this.load();
    }

    public get displayDate(): string {
        return moment(this.date).format('LL');
    }

    public isExpense(transaction: Transaction): boolean {
        return transaction instanceof Expense;
    }

    private load(): void {
        this.isLoading = true;
        let request = {
            startDate: moment(this.date).toDate().toLocaleDateString(),
            endDate: moment(this.date)
                .add(1, 'days')
                .toDate()
                .toLocaleDateString(),
        };

        let getExpenses = this.apiHttpService
            .get(this.apiEndpointsService.getExpensesEndpoint(request))
            .pipe<Transaction[]>(
                map((data: any) => data.map((x: any) => new Expense(x)))
            );

        let getIncomes = this.apiHttpService
            .get(this.apiEndpointsService.getIncomesEndpoint(request))
            .pipe<Transaction[]>(
                map((data: any) => data.map((x: any) => new Income(x)))
            );

        forkJoin([getExpenses, getIncomes])
            .pipe(finalize(() => (this.isLoading = false)))
            .subscribe((result) => {
                this.transactions = [].concat.apply([], result);
            });
    }
}
