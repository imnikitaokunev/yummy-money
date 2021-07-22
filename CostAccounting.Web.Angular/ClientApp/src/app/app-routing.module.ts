import { TransactionsComponent } from './modules/home/pages/transactions/transactions.component';
import { HelpComponent } from "./modules/home/pages/help/help.component";
import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./modules/home/pages/home/home.component";
import { LoginComponent } from "./modules/home/pages/login/login.component";
import { AuthGuard } from './core/guards/auth.guard';
import { RegisterComponent } from './modules/home/pages/register/register.component';
import { ReportsComponent } from './modules/home/pages/reports/reports.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "transactions", component: TransactionsComponent},
  { path: "reports", component: ReportsComponent, canActivate: [AuthGuard] },
  { path: "help", component: HelpComponent, canActivate: [AuthGuard] },
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule],
})
export class AppRoutingModule {}
