import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";

@Component({
    selector: "app-checkbox",
    templateUrl: `checkbox.component.html`,
})
export class CheckboxComponent implements OnInit {
    @Input() name: string = null;
    @Input() label: string = null;
    @Output() checked = new EventEmitter<boolean>();

    //public isChecked: boolean;

    onChecked(isChecked: boolean): void {
        this.checked.emit(isChecked);
    }

    constructor() {}

    ngOnInit(): void {}
}
