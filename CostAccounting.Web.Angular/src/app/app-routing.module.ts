import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./modules/home/pages/home/home.component";
import { LoginComponent } from "./modules/home/pages/login/login.component";

const routes: Routes = [
    { path: "", component: HomeComponent },
    { path: "login", component: LoginComponent },
];

// Move this to home-routing.module.ts

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
