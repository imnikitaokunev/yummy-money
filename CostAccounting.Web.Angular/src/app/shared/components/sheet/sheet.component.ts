import { Expense } from "src/app/core/models/expense";
import { Component, Input, OnInit } from "@angular/core";
import { Income } from "src/app/core/models/income";

@Component({
    selector: "app-sheet",
    templateUrl: "sheet.component.html",
})
export class SheetComponent implements OnInit {
    @Input() date: Date;
    @Input() day: number;
    @Input() isToday: boolean;
    @Input() isCurrentMonth: boolean;
    @Input() expenses: Expense[];
    @Input() incomes: Income[];

    constructor() {}

    ngOnInit(): void {}
}
