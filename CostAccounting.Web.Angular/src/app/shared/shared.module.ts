import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { CheckboxComponent } from "./components/checkbox/checkbox.component";
import { PasswordInputComponent } from "./components/password-input/password-input.component";
import { UsernameInputComponent } from "./components/username-input/username-input.component";
import { InputComponent } from "./components/input/input.component";

@NgModule({
    imports: [FormsModule, BrowserModule],
    declarations: [CheckboxComponent, PasswordInputComponent, UsernameInputComponent, InputComponent],
    exports: [CheckboxComponent, PasswordInputComponent, UsernameInputComponent, InputComponent],
})
export class SharedModule {}
