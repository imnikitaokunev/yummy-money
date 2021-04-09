import { HeaderComponent } from "./core/header/header.component";
import { CoreModule } from "./core/core.module";
import { HomeModule } from "./modules/home/home.module";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { HttpClientModule } from "@angular/common/http";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, HomeModule, AppRoutingModule, HttpClientModule, CoreModule, NgbModule],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
