import { Component, OnInit, Input, ViewChild, ElementRef } from "@angular/core";
import { InputComponent } from "../input/input.component";

@Component({
    selector: "app-password-input",
    templateUrl: "password-input.component.html",
})
export class PasswordInputComponent extends InputComponent {
    @Input() linkText: string = null;
    @Input() href: string = null;

    public showPassword: boolean;

    constructor() {
        super();
    }

    onShowPassword(showPassword: boolean): void {
        this.showPassword = showPassword;
    }
}
