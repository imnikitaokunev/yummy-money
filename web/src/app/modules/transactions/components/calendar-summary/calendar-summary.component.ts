import { Transaction } from 'src/app/core/models/transaction';
import {
    Component,
    EventEmitter,
    Input,
    OnChanges,
    OnInit,
    Output,
    SimpleChanges,
} from '@angular/core';

@Component({
    selector: 'app-calendar-summary',
    templateUrl: './calendar-summary.component.html',
})
export class CalendarSummaryComponent implements OnInit, OnChanges {
    @Input() transactions: Transaction[] = [];
    @Input() isLoading: boolean;
    @Input() isError: boolean;

    @Output() reloaded = new EventEmitter<void>();

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

    public reload(): void {
        this.reloaded.emit();
    }

    private calculate(): void {
        this.expensesSum = this.getArraySum(
            this.transactions.filter((x) => !x.isIncome)
        );
        this.incomesSum = this.getArraySum(
            this.transactions.filter((x) => x.isIncome)
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
