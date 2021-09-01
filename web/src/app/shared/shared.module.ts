import { NgModule } from '@angular/core';
import { LoadingIndicatorSmallComponent } from './components/loading-indicator-small/loading-indicator-small.component';
import { LoadingIndicatorComponent } from './components/loading-indicator/loading-indicator.component';
import { HoverClassDirective } from './directives/hover-class.directive';

@NgModule({
    declarations: [LoadingIndicatorSmallComponent, LoadingIndicatorComponent, HoverClassDirective],
    exports: [LoadingIndicatorSmallComponent, LoadingIndicatorComponent, HoverClassDirective],
})
export class SharedModule {}
