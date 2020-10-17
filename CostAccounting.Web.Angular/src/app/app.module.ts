import { HeaderComponent } from "./core/header/header.component";
import { CoreModule } from "./core/core.module";
import { HomeModule } from "./modules/home/home.module";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, HomeModule, AppRoutingModule, HttpClientModule, CoreModule],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
