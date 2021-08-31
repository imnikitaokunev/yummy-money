import { Component, Input, OnInit } from '@angular/core';
import { Expense } from 'src/app/core/models/expense';
import { Transaction } from 'src/app/core/models/transaction';

@Component({
    selector: 'app-sheet',
    templateUrl: './sheet.component.html',
})
export class SheetComponent implements OnInit {
    @Input() date: Date = new Date(Date.now());
    @Input() day: number;
    @Input() isToday: boolean;
    @Input() isCurrentMonth: boolean;
    @Input() transactions: Transaction[] = [];

    constructor() {}

    ngOnInit(): void {}

    public isExpense(transaction: Transaction): boolean{
        return transaction instanceof Expense;
    }
}
