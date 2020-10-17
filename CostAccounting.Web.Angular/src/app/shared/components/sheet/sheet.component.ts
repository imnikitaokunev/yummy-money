import { Component, Input, OnInit } from "@angular/core";

@Component({
    selector: "app-sheet",
    templateUrl: "sheet.component.html",
})
export class SheetComponent implements OnInit {
    @Input() date: Date;
    @Input() day: number;
    @Input() isToday: boolean;
    @Input() isCurrentMonth: boolean;

    constructor() {}

    ngOnInit(): void {}
}
