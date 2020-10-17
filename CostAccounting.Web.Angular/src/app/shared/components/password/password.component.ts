import { InputComponent } from "./../input/input.component";
import { Component, Input, OnInit } from "@angular/core";

@Component({
    selector: "app-password",
    templateUrl: "password.component.html",
})
export class PasswordComponent extends InputComponent implements OnInit {
    @Input() showPassword: boolean;

    constructor() {
        super();
    }

    ngOnInit(): void {}
}
