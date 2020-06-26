import { Component, Input } from "@angular/core";

@Component({
    selector: "app-input",
    templateUrl: "input.component.html",
})
export class InputComponent {
    @Input() name: string;

    public isFocused: boolean;

    public onFocus(isFocused: boolean): void {
        this.isFocused = isFocused;
    }

    constructor() {}
}
