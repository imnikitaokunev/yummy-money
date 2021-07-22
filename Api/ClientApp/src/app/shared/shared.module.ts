import { NgModule } from '@angular/core';
import { LoadingIndicatorSmallComponent } from './components/loading-indicator-small/loading-indicator-small.component';
import { LoadingIndicatorComponent } from './components/loading-indicator/loading-indicator.component';

@NgModule({
    declarations: [LoadingIndicatorSmallComponent, LoadingIndicatorComponent],
    exports: [LoadingIndicatorSmallComponent, LoadingIndicatorComponent],
})
export class SharedModule {}
