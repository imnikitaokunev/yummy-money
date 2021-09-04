import { SharedModule } from 'src/app/shared/shared.module';
import { TransactionsRoutingModule } from './transactions-routing.module';
import { TransactionsComponent } from './pages/transactions/transactions.component';
import { NgModule } from '@angular/core';
import { CoreModule } from 'src/app/core/core.module';
import { SheetComponent } from './components/sheet/sheet.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CalendarSummaryComponent } from './components/calendar-summary/calendar-summary.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { AddTransactionComponent } from './components/add-transaction/add-transaction.component';
import { ViewTransactiosComponent } from './components/view-transactios/view-transactios.component';

@NgModule({
    imports: [FormsModule, CommonModule, CoreModule, TransactionsRoutingModule, SharedModule, ReactiveFormsModule],
    declarations: [TransactionsComponent, SheetComponent, CalendarSummaryComponent, CalendarComponent, AddTransactionComponent, ViewTransactiosComponent],
})
export class TransactionsModule {}
