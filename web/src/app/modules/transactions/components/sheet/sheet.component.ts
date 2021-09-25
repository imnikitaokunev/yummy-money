import { Component, Input, OnInit } from '@angular/core';
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
}
