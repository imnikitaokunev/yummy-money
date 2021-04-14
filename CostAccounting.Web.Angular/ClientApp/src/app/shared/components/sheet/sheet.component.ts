import { ModalService } from "./../../../core/services/modal.service";
import { TransactionModalComponent } from "./../transaction-modal/transaction-modal.component";
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
    @Input() expenses: Expense[] = [];
    @Input() incomes: Income[] = [];

    public readonly sheetItemsCount: number = 5;

    constructor(private modalService: ModalService) {}

    ngOnInit(): void {}

    public openModal(): void {
        this.modalService.open(TransactionModalComponent, { ariaLabelledBy: "modal-basic-title" }, this);
    }
}
