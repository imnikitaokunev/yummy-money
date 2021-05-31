import { ReportsComponent } from './pages/reports/reports.component';
import { TransactionsComponent } from "./pages/transactions/transactions.component";
import { HelpComponent } from "./pages/help/help.component";
import { LoginComponent } from "./pages/login/login.component";
import { HomeComponent } from "./pages/home/home.component";
import { SharedModule } from "./../../shared/shared.module";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { RegisterComponent } from "./pages/register/register.component";
import { ChartsModule } from 'ng2-charts';

@NgModule({
  imports: [CommonModule, SharedModule, FormsModule, BrowserModule, ChartsModule],
  declarations: [
    HomeComponent,
    TransactionsComponent,
    ReportsComponent,
    LoginComponent,
    HelpComponent,
    RegisterComponent,
  ],
})
export class HomeModule {}
