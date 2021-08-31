import { HomeRoutingModule } from './home-routing.module';
import { NgModule } from '@angular/core';
import { HomeComponent } from './pages/home/home.component';
import { CoreModule } from 'src/app/core/core.module';

@NgModule({
    declarations: [HomeComponent],
    imports: [CoreModule, HomeRoutingModule],
})
export class HomeModule {}
