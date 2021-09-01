import { Transaction } from './../../../../core/models/transaction';
import {
    Component,
    EventEmitter,
    Input,
    OnChanges,
    OnInit,
    Output,
    SimpleChanges,
} from '@angular/core';
import { Expense } from 'src/app/core/models/expense';
import { Income } from 'src/app/core/models/income';

@Component({
    selector: 'app-calendar-summary',
    templateUrl: './calendar-summary.component.html',
})
export class CalendarSummaryComponent implements OnInit, OnChanges {
    @Input() transactions: Transaction[] = [];
    @Input() isLoading: boolean;
    @Input() isError: boolean;

    @Output() refreshed = new EventEmitter<boolean>();

    public incomesSum: number;
    public expensesSum: number;
    public net: number;
    public netPercent: number;

    constructor() {}

    ngOnInit(): void {}

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.transactions) {
            this.calculate();
        }
    }

    public refresh(): void {
        this.refreshed.emit();
    }

    private calculate(): void {
        this.expensesSum = this.getArraySum(
            this.transactions.filter((x) => x instanceof Expense)
        );
        this.incomesSum = this.getArraySum(
            this.transactions.filter((x) => x instanceof Income)
        );
        this.net = this.incomesSum - this.expensesSum;
        this.netPercent = this.incomesSum
            ? this.net / this.incomesSum
            : this.net
            ? -1
            : 0;
    }

    private getArraySum(array: { amount: number }[]) {
        return !array ? 0 : array.reduce((sum, value) => sum + value.amount, 0);
    }
}
