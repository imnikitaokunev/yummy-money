import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { PasswordInputComponent } from "./password-input/password-input.component";
import { InputComponent } from "./input/input.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { UsernameInputComponent } from "./username-input/username-input.component";
import { CheckboxComponent } from "./checkbox/checkbox.component";

@NgModule({
    declarations: [
        AppComponent,
        PasswordInputComponent,
        InputComponent,
        HomeComponent,
        LoginComponent,
        UsernameInputComponent,
        CheckboxComponent,
    ],
    imports: [BrowserModule, AppRoutingModule, FormsModule],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
