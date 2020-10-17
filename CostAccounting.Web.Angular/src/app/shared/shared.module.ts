import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { CheckboxComponent } from "./components/checkbox/checkbox.component";
import { InputComponent } from "./components/input/input.component";
import { PasswordComponent } from "./components/password/password.component";
import { SheetComponent } from "./components/sheet/sheet.component";
import { CalendarComponent } from "./components/calendar/calendar.component";
import { LoadingIndicatorComponent } from "./components/loading-indicator/loading-indicator.component";

@NgModule({
    imports: [FormsModule, BrowserModule],
    declarations: [
        CheckboxComponent,
        InputComponent,
        PasswordComponent,
        SheetComponent,
        CalendarComponent,
        LoadingIndicatorComponent,
    ],
    exports: [
        CheckboxComponent,
        InputComponent,
        PasswordComponent,
        SheetComponent,
        CalendarComponent,
        LoadingIndicatorComponent,
    ],
})
export class SharedModule {}
