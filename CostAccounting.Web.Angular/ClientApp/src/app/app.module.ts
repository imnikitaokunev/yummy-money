import { Router } from '@angular/router';
// import { HomeRoutingModule } from './modules/home/home-routing.module';
import { HeaderComponent } from "./core/header/header.component";
import { CoreModule } from "./core/core.module";
import { HomeModule } from "./modules/home/home.module";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { HttpClientModule } from "@angular/common/http";
import { JwtModule } from "@auth0/angular-jwt";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ACCESS_TOKEN_KEY } from "./core/services/auth.service";

export function tokenGetter(){
    return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HomeModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    NgbModule,

    JwtModule.forRoot({
      config: {
        tokenGetter
      },
    })
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
