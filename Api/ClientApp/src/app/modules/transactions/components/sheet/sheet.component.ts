import { Component, Input, OnInit } from '@angular/core';
import { Expense } from 'src/app/core/models/expense';
import { Income } from 'src/app/core/models/income';

@Component({
    selector: 'app-sheet',
    templateUrl: './sheet.component.html',
})
export class SheetComponent implements OnInit {
    @Input() date: Date = new Date(Date.now());
    @Input() day: number;
    @Input() isToday: boolean;
    @Input() isCurrentMonth: boolean;
    @Input() expenses: Expense[] = [];
    @Input() incomes: Income[] = [];

    public readonly sheetItemsCount: number = 5;

    constructor() {}

    ngOnInit(): void {}
}
