import {
    Component,
    Input,
    OnChanges,
    OnInit,
    SimpleChanges,
} from '@angular/core';
import { Expense } from 'src/app/core/models/expense';
import { Income } from 'src/app/core/models/income';

@Component({
    selector: 'app-calendar-summary',
    templateUrl: './calendar-summary.component.html',
})
export class CalendarSummaryComponent implements OnInit, OnChanges {
    @Input() expenses: Expense[] = [];
    @Input() incomes: Income[] = [];
    @Input() isLoading: boolean;

    public incomesSum: number;
    public expensesSum: number;
    public net: number;
    public netPercent: number;

    constructor() {}
    ngOnInit(): void {}

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.expenses || changes.incomes) {
            this.calculate();
        }
    }

    private calculate(): void {
        this.expensesSum = this.getArraySum(this.expenses);
        this.incomesSum = this.getArraySum(this.incomes);
        this.net = this.incomesSum - this.expensesSum;
        this.netPercent = this.incomesSum
            ? this.net / this.incomesSum
            : this.net
            ? -1
            : 0;
    }

    private getArraySum(array: { amount: number }[]) {
        // if (!array) {
        //     return 0;
        // }

        return !array ? 0 : array.reduce((a, b) => a + b.amount, 0);
    }
}
