import { RouterModule } from '@angular/router';
import { ApiEndpointsService } from './services/api-endpoints.service';
import { ApiHttpService } from './services/api-http.service';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';

@NgModule({
    imports: [HttpClientModule, RouterModule],
    declarations: [HeaderComponent],
    providers: [ApiHttpService, ApiEndpointsService],
    exports: [HeaderComponent],
})
export class CoreModule {}
