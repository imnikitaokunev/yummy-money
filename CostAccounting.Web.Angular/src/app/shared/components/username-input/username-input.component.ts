import { InputComponent } from "./../input/input.component";
import { Component, OnInit, Input, ViewChild, ElementRef } from "@angular/core";

@Component({
    selector: "app-username-input",
    templateUrl: "username-input.component.html",
})
export class UsernameInputComponent extends InputComponent {
    @Input() linkText: string = null;
    @Input() href: string = null;

    constructor() {
        super();
    }
}
