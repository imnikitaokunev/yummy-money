import { HomeModule } from './modules/home/home.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { TransactionsModule } from './modules/transactions/transactions.module';
import { JwtModule } from '@auth0/angular-jwt';
import { tokenGetter } from './core/services/auth.service';

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HomeModule,
        CoreModule,
        TransactionsModule,
        JwtModule.forRoot({
            config: {
                tokenGetter,
            },
        }),
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
