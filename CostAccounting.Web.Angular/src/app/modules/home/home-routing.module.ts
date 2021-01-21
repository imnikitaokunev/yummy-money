import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./pages/home/home.component";
import { LoginComponent } from "./pages/login/login.component";
import { NgModule } from "@angular/core";
import { HelpComponent } from "./pages/help/help.component";

const routes: Routes = [
    { path: "", component: HomeComponent },
    { path: "login", component: LoginComponent },
    { path: "help", component: HelpComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class HomeRoutingModule {}
