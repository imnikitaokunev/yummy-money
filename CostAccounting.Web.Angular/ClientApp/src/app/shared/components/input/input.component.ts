import { BaseInputComponent } from "./../base-input.component";
import { Component, Input, OnInit } from "@angular/core";

@Component({
    selector: "app-input",
    templateUrl: "input.component.html",
})
export class InputComponent extends BaseInputComponent implements OnInit {
    @Input() placeholder: string;

    public isFocused: boolean = false;

    public onFocus(isFocused: boolean): void {
        this.isFocused = isFocused;
    }

    constructor() {
        super();
    }

    ngOnInit(): void {}
}
