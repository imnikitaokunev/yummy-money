import { RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { HeaderComponent } from "./header/header.component";
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
    declarations: [HeaderComponent],
    imports: [RouterModule, BrowserModule],
    exports: [HeaderComponent],
})
export class CoreModule {}
