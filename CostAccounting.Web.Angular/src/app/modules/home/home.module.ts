import { HelpComponent } from "./pages/help/help.component";
import { LoginComponent } from "./pages/login/login.component";
import { HomeComponent } from "./pages/home/home.component";
import { SharedModule } from "./../../shared/shared.module";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

@NgModule({
    imports: [CommonModule, SharedModule],
    declarations: [HomeComponent, LoginComponent, HelpComponent],
})
export class HomeModule {}
