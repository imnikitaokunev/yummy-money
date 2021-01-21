import { BaseInputComponent } from "./../base-input.component";
import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";

@Component({
    selector: "app-checkbox",
    templateUrl: `checkbox.component.html`,
})
export class CheckboxComponent extends BaseInputComponent implements OnInit {
    @Output() checked = new EventEmitter<boolean>();

    onChecked(isChecked: boolean): void {
        this.checked.emit(isChecked);
    }

    constructor() {
        super();
    }

    ngOnInit(): void {}
}
